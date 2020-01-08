using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Db;
using Domain.Client;
using Domain.Network;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetworkUtils.Configs;
using NetworkUtils.Utils;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Ip.Firewall;

namespace NetworkUtils.Services
{
    public class NatConfigService
    {
        private readonly ILogger<NatConfigService> _logger;
        private readonly RouterConfig _routerConfig;
        private readonly RouterUtils _routerUtils;
        private const string SrcNatChain = "srcnat";
        private const string DstNatChain = "dstnat";
        private const string SrcNatAction = "src-nat";
        private const string DstNatAction = "dst-nat";
        private const string WanInterface = "ether6";

        public NatConfigService(ILogger<NatConfigService> logger, IOptionsMonitor<RouterConfig> routerConfig)
        {
            _logger = logger;
            _routerConfig = routerConfig.CurrentValue;
            _routerUtils = new RouterUtils(logger);
        }

        public void Generate()
        {
            _logger.LogDebug("#Start nat config generator");
            using (var db = new ApplicationDbContext())
            {
                var activeClients = ClientUtils.GetActiveClients(db);

                var allGrey = activeClients.Select(x => x.GreyAddress)
                    .ToImmutableHashSet();

                using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {
                    connection.OnReadRow += _routerUtils.Connection_OnReadRow; // logging commands to cosole
                    connection.OnWriteRow += _routerUtils.Connection_OnWriteRow; // logging commands to cosole
                    connection.Open(_routerConfig.Host, _routerConfig.Username, _routerConfig.Password);
                    var natRules = connection.LoadAll<FirewallNat>()
                        .ToList();
                    RemoveNotActualRules(natRules, allGrey, connection);
                    AddNewRules(activeClients, natRules, connection);
                }
            }

            _logger.LogDebug("#End nat config generator");
        }

        private void AddNewRules(List<Client> activeClients, List<FirewallNat> natRules, ITikConnection connection)
        {
            foreach (var client in activeClients)
            {
                var grey = client.GreyAddress.Address.Replace(" ", string.Empty);
                var white = client.GreyAddress.White.Address.Replace(" ", string.Empty);
                var login = "\"" + client.Login + "\"";

                var srcRule = natRules
                    .Where(rule => rule.Action == SrcNatAction)
                    .FirstOrDefault(rule => rule.ToAddresses == white && rule.SrcAddress == grey);

                if (srcRule == null)
                {
                    _logger.LogDebug("Need create src nat rule for login: {0}, grey {1}, white {2}", client.Login, grey, white);
                    var rule = new FirewallNat
                    {
                        Chain = SrcNatChain,
                        Action = SrcNatAction,
                        OutInterface = WanInterface,
                        ToAddresses = white,
                        SrcAddress = grey,
                        Comment = login
                    };

                    connection.Save(rule);
                }

                var dstRule = natRules
                    .Where(rule => rule.Action == DstNatAction)
                    .FirstOrDefault(rule => rule.ToAddresses == grey && rule.DstAddress == white);

                if (dstRule == null)
                {
                    _logger.LogDebug("Need create dst nat rule for login: {0}, grey {1}, white {2}", client.Login, grey, white);
                    var rule = new FirewallNat
                    {
                        Chain = DstNatChain,
                        Action = DstNatAction,
                        InInterface = WanInterface,
                        ToAddresses = grey,
                        DstAddress = white,
                        Comment = login
                    };

                    connection.Save(rule);
                }
            }
        }

        private void RemoveNotActualRules(List<FirewallNat> natRules, ImmutableHashSet<GreyAddress> allGrey, ITikConnection connection)
        {
            var toRemoveDst = GetToRemoveDstRules(natRules, allGrey);
            var toRemoveSrc = ToRemoveSrcRules(natRules, allGrey);
            foreach (var rule in toRemoveDst.Concat(toRemoveSrc))
            {
                RemoveNatRule(connection, rule);
            }
        }

        private static List<FirewallNat> ToRemoveSrcRules(List<FirewallNat> natRules, ImmutableHashSet<GreyAddress> allGrey)
        {
            return natRules
                .Where(rule => !rule.Comment.Contains("iddqd"))
                .Where(rule => rule.Action == SrcNatAction)
                .Where(rule => !allGrey.Any(address => address.Address == rule.SrcAddress && address.White.Address == rule.ToAddresses))
                .ToList();
        }

        private static List<FirewallNat> GetToRemoveDstRules(List<FirewallNat> natRules, ImmutableHashSet<GreyAddress> allGrey)
        {
            return natRules
                .Where(rule => !rule.Comment.Contains("iddqd"))
                .Where(rule => rule.Action == DstNatAction)
                .Where(rule => !allGrey.Any(x => x.Address == rule.ToAddresses && x.White.Address == rule.DstAddress))
                .ToList();
        }

        private void RemoveNatRule(ITikConnection connection, FirewallNat rule)
        {
            _logger.LogDebug("rule with coment: {0}, action {1}, ToAddresses: {2} will be removed", rule.Comment, rule.Action, rule.ToAddresses);
            // WriteLine("Fake remove");
            connection.Delete(rule);
        }
    }
}