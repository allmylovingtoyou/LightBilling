using System.Linq;
using Db;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace NetworkUtils.Services
{
    public class NatConfigService
    {
        public void Generate()
        {
            WriteLine("#Start nat config generator");
            using (var db = new ApplicationDbContext())
            {
                var clients = db.Clients
                    .Include(c => c.GreyAddress)
                    .ThenInclude(g => g.White)
                    .Where(c => c.GreyAddress != null)
                    .Where(c => c.GreyAddress.White != null)
                    .ToList();

                clients.ForEach(c =>
                {
                    var grey = c.GreyAddress.Address;
                    var white = c.GreyAddress.White.Address;
                    WriteLine($"#2-way mapping for {c.Login}");
                    WriteLine($"/sbin/iptables -t nat -A PREROUTING -d {white} -j DNAT --to-destination {grey}");
                    WriteLine($"/sbin/iptables -t nat -I POSTROUTING 1 -s {grey} -o eth0.52 -j SNAT --to-source {white}");
                    WriteLine();
                });
            }

            WriteLine("#End nat config generator");
        }
    }
}