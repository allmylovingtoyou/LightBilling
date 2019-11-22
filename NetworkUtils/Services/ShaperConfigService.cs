using System.IO;
using System.Linq;
using Api.Client.Status;
using Db;
using LightBilling.Mapping;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace NetworkUtils.Services
{
    public class ShaperConfigService
    {
        public void Generate()
        {
            using (var db = new ApplicationDbContext())
            {
                string template = File.ReadAllText(@"templates/shaper_rules_default.sh");
                WriteLine(template);
                WriteLine();
                WriteLine("#Start shaper config generator");
                
                var clients = db.Clients
                    .Include(c => c.GreyAddress)
                    .Include(c => c.JoinTariffs)
                    .ThenInclude(t => t.Tariff)
                    .Where(c => !c.IsDeleted)
                    .Where(c => c.GreyAddress != null)
                    .Where(c => c.GreyAddress.White != null)
                    .ToList();

                var flowId = 20;
                clients.ForEach(c =>
                {
                    var grey = c.GreyAddress.Address;
                    //TODO
                    var tariff = c.JoinTariffs
                        .Select(jt => jt.Tariff)
                        .FirstOrDefault(t => t.IsPeriodic);

                    var speed = tariff?.InputRate * 1024 ?? 1024;

                    WriteLine($"#shaper rules for {c.Login}");
                    WriteLine($"#speed = {speed}");
                    WriteLine($"/sbin/tc class add dev eth1 parent 1:11 classid 1:{flowId} htb rate {speed}Kbit");
                    WriteLine($"/sbin/tc class add dev ifb0 parent 1:11 classid 1:{flowId} htb rate {speed}Kbit");
                   
                    WriteLine($"/sbin/tc qdisc add dev eth1 handle {flowId}: parent 1:{flowId} sfq");
                    WriteLine($"/sbin/tc qdisc add dev ifb0 handle {flowId}: parent 1:{flowId} sfq");
                    WriteLine();
                    
                    WriteLine($"/sbin/tc filter add dev eth1 parent 1:11 protocol all prio 1 u32 match ip dst {grey}/32 flowid 1:{flowId}");
                    WriteLine($"/sbin/tc filter add dev ifb0 parent 1:11 protocol all prio 1 u32 match ip src {grey}/32 flowid 1:{flowId}");
                    WriteLine();

                    flowId++;
                });
            }

            WriteLine("#End shaper config generator");
        }
    }
}