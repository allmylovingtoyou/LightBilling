using System;
using System.Collections.Generic;
using System.Linq;
using Db;
using Domain.Base;
using Domain.Client;
using Domain.House;
using Domain.Network;
using Domain.Tariff;

namespace DevelopData.Entity
{
    public static class ClientDevData
    {
        public static readonly List<Client> Clients = new List<Client>();

        public static void Create()
        {
            var client1 = (new Client()
            {
                Name = "Вася",
                Surname = "Петров",
                Patronymic = "Сысоевич",
            });

            var tariff1 = TariffDevData.DevDataTariffs.First();
            client1.JoinTariffs = new List<JoinClientsTariffs>
            {
                new JoinClientsTariffs
                {
                    Client = client1,
                    TariffId = tariff1.Id
                }
            };

            client1.HouseId = HousesDevData.Houses.First().Id;
            client1 = Save(client1);

//            for (int i = 0; i < 20; i++)
//            {
//                tariffs.Add(new Tariff
//                {
//                    Name = "Рандом " + i,
//                    IsPeriodic = true,
//                    InputRate = 777 + i,
//                    OutputRate = 888 + i,
//                    Cost = 500 + i,
//                    Type = TariffType.Tariff
//                });
//            }

            using (var db = new ApplicationDbContext())
            {
                db.Clients.AddRange(Clients);
                db.SaveChanges();
            }

            Clients.Add(client1);
        }

        private static T Save<T>(T entity) where T : class, IBaseEntity
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Set<T>().Add(entity);
                db.SaveChanges();

                return result.Entity;
            }
        }
    }
}