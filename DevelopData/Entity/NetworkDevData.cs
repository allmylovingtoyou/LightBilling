using System.Collections.Generic;
using Db;
using Domain.House;
using Domain.Network;

namespace DevelopData.Entity
{
    public static class NetworkDevData
    {
        public static List<Subnet> Create()
        {
            var subnets = new List<Subnet>();

            for (int i = 10; i < 100; i++)
            {
                var subnet = new Subnet
                {
                    Net = "192.168." + i + ".0",
                    Mask = 24,
                    Gateway = "192.168." + i + ".1"
                };

                var greys = new List<GreyAddress>();
                for (int j = 5; j < 15; j++)
                {
                    var grey = new GreyAddress
                    {
                        Address = "192.168." + i + "." + j,
                        White = new WhiteAddress {Address = "95.77." + i + "." + j}
                    };

                    greys.Add(grey);
                }

                subnet.Addresses = greys;

                subnets.Add(subnet);
            }

            using (var db = new ApplicationDbContext())
            {
                db.Subnets.AddRange(subnets);
                db.SaveChanges();
            }

            return subnets;
        }
    }
}