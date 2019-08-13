using System.Collections.Generic;
using Db;
using Domain.House;
using Domain.Network;

namespace DevelopData.Entity
{
    public static class HousesDevData
    {
        public static readonly List<House> Houses = new List<House>();

        public static void Create()
        {
            Houses.Add(new House
            {
                Address = "Дровосека",
                Number = "15",
                Porch = "1-10",
                Comment = "Наркоманы в подъезде",
                Subnet = new Subnet
                {
                    Net = "192.168.1.0",
                    Mask = 24,
                    Gateway = "192.168.1.1"
                }
            });

            Houses.Add(new House
            {
                Address = "Девопсическа",
                Number = "66"
            });

            for (int i = 0; i < 20; i++)
            {
                Houses.Add(new House
                {
                    Address = "Рандома",
                    Number = i.ToString()
                });
            }

            using (var db = new ApplicationDbContext())
            {
                db.Houses.AddRange(Houses);
                db.SaveChanges();
            }
        }
    }
}