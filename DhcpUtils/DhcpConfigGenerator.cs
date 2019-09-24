using System;
using System.Linq;
using Db;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace NetworkUtils
{
    public class DhcpConfigGenerator
    {
        public void Generate()
        {
            using (var db = new ApplicationDbContext())
            {
                var clients = db.Clients
                    .Include(c => c.House)
                    .Where(c => c.GreyAddress != null)
                    .Where(c => c.House != null)
                    .Where(c => c.House.Subnet != null);

                var grouped = clients.GroupBy(x => x.House.Subnet);

                WriteLine("group");
                WriteLine("{");
                foreach (var pair in grouped)
                {
                    var net = pair.Key;
                    WriteLine($"    subnet {net.Net} netmask {MapNetMask(net.Mask)}");
                    WriteLine("    {");
                    WriteLine($"        option routers {net.Gateway};");
                    WriteLine($"        option subnet-mask {MapNetMask(net.Mask)};");

                    foreach (var cl in pair.Select(x => x)
                        .Where(x => !string.IsNullOrEmpty(x.MacAddress))
                        .Where(x => !string.IsNullOrEmpty(x.GreyAddress.Address))
                    )
                    {
                        Write($"        host GEN_{cl.Login} ");
                        Write("{ ");
                        Write($"hardware ethernet {cl.MacAddress}; fixed-address {cl.GreyAddress.Address};");
                        Write("}");
                        WriteLine();
                    }
                    WriteLine("    }");
                }
                WriteLine("}");
                WriteLine();
            }
        }

        public string MapNetMask(int mask)
        {
            if (mask == 27)
            {
                return "255.255.255.224";
            }
            throw new InvalidOperationException();
        }
    }
}