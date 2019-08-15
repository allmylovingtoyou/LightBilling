using System.Collections.Generic;
using System.Linq;
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
                SubnetId = NetworkDevData.Subnets.First().Id
            });

            Houses.Add(new House
            {
                Address = "Девопсическа",
                Number = "66"
            });

            for (int i = 0; i < 11; i++)
            {
                Houses.Add(new House
                {
                    Address = "Рандома",
                    Number = i.ToString(),
                    SubnetId = NetworkDevData.Subnets[i + 1].Id
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