using System.Collections.Immutable;
using System.Linq;
using Db;
using Domain.Tariff;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetworkUtils.Configs;
using NetworkUtils.Utils;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Queue;
using static System.Console;

namespace NetworkUtils.Services
{
    public class ShaperConfigService
    {
        private readonly ILogger<ShaperConfigService> _logger;
        private readonly RouterConfig _routerConfig;
        private readonly RouterUtils _routerUtils;

        public ShaperConfigService(ILogger<ShaperConfigService> logger, IOptionsMonitor<RouterConfig> routerConfig)
        {
            _logger = logger;
            _routerConfig = routerConfig.CurrentValue;
            _routerUtils = new RouterUtils(logger);
        }

        public void Generate()
        {
            using (var db = new ApplicationDbContext())
            {
                var activeClients = ClientUtils.GetActiveClients(db);
                var clientsData = activeClients.Select(client =>
                    {
                        //TODO
                        var tariff = client.JoinTariffs
                            .Select(jt => jt.Tariff)
                            .FirstOrDefault(t => t.Type.Equals(TariffType.Tariff));

                        var speed = tariff?.InputRate ?? 1;
                        return (Address: client.GreyAddress.Address, Speed: speed, Login: client.Login);
                    })
                    .ToImmutableHashSet();

                using (var connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
                {
                    connection.OnReadRow += _routerUtils.Connection_OnReadRow; // logging commands to cosole
                    connection.OnWriteRow += _routerUtils.Connection_OnWriteRow; // logging commands to cosole
                    connection.Open(_routerConfig.Host, _routerConfig.Username, _routerConfig.Password);
                    var shaperRules = connection.LoadAll<QueueSimple>()
                        .ToList();

                    var toRemove = shaperRules
                        .Where(rule => !clientsData.Any(clientData => CreateRawAddress(clientData) == rule.Target
                                                                      && CreateRawSpeed(clientData.Speed) == rule.MaxLimit));

                    foreach (var item in toRemove)
                    {
                        RemoveShaperRule(connection, item);
                    }

                    foreach (var clientData in clientsData)
                    {
                        var shaperRule = shaperRules.FirstOrDefault(rule => rule.Target == CreateRawAddress(clientData)
                                                                            && CreateRawSpeed(clientData.Speed) == rule.MaxLimit);

                        if (shaperRule == null)
                        {
                            _logger.LogDebug("Need create rule for login: {0}, grey {1}, speed {2}M",
                                clientData.Login, clientData.Address, clientData.Speed);
                            var rule = new QueueSimple
                            {
                                MaxLimit = $"{clientData.Speed}M/{clientData.Speed}M",
                                Name = clientData.Login,
                                Queue = "default/default",
                                Target = clientData.Address
                            };
                            connection.Save(rule);
                        }
                    }
                }

                // WriteLine($"add max-limit={speed}M/{speed}M name=\"{c.Login}\" queue=default/default target={grey}");
            }

            WriteLine("#End shaper config generator");
        }

        private static string CreateRawAddress((string Address, int Speed, string Login) clientData)
        {
            return clientData.Address + "/32";
        }

        private static string CreateRawSpeed(int speed)
        {
            var routerFormat = speed * 1000000;
            return $"{routerFormat}/{routerFormat}";
        }

        private void RemoveShaperRule(ITikConnection connection, QueueSimple rule)
        {
            _logger.LogDebug("rule with coment: {0}, Target {1}, MaxLimit: {2} will be removed", rule.Comment, rule.Target, rule.MaxLimit);
            connection.Delete(rule);
        }
    }
}