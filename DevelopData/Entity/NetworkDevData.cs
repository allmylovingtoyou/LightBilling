using System;
using System.Collections.Generic;
using Db;
using Domain.Network;

namespace DevelopData.Entity
{
    public static class NetworkDevData
    {
        private static Random _random = new Random();
        public static readonly List<Subnet> Subnets = new List<Subnet>();

        public static void Create()
        {
            for (int i = 10; i < 40; i++)
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

                    var value = _random.Next(1, 3);
                    if (value == 1)
                    {
                        grey.White = null;
                    }

                    greys.Add(grey);
                }

                subnet.Addresses = greys;

                Subnets.Add(subnet);
            }

            var whiteAddresses = new List<WhiteAddress>();
            for (var j = 1; j < 40; j++)
            {
                whiteAddresses.Add(new WhiteAddress
                {
                    Address = "99.78." + 48 + "." + j,
                });
            }


            using (var db = new ApplicationDbContext())
            {
                db.Subnets.AddRange(Subnets);
                db.WhiteAddresses.AddRange(whiteAddresses);
                db.SaveChanges();
            }
        }
    }
}