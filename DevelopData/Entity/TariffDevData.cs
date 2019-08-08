using System.Collections.Generic;
using Db;
using Domain.House;
using Domain.Network;
using Domain.Tariff;

namespace DevelopData.Entity
{
    public static class TariffDevData
    {
        
        public static List<Tariff> Create()
        {
            var tariffs = new List<Tariff>();
            
            tariffs.Add(new Tariff
            {
                Name = "Улет 100500",
                IsPeriodic = true,
                InputRate = 512,
                OutputRate = 512,
                Cost = 100500,
                Type = TariffType.Tariff
            });
            
            tariffs.Add(new Tariff
            {
                Name = "Белый адрес",
                IsPeriodic = true,
                Cost = 500,
                Type = TariffType.Any
            });
            
            tariffs.Add(new Tariff
            {
                Name = "Подключение инета",
                IsPeriodic = false,
                Cost = 666,
                Type = TariffType.Any
            });


            for (int i = 0; i < 20; i++)
            {
                tariffs.Add(new Tariff
                {
                    Name = "Рандом " + i,
                    IsPeriodic = true,
                    InputRate = 777 + i,
                    OutputRate = 888 + i,
                    Cost = 500 + i,
                    Type = TariffType.Tariff
                });
            }
            
            using (var db = new ApplicationDbContext())
            {
                db.Tariffs.AddRange(tariffs);
                db.SaveChanges();
            }

            return tariffs;
        }

    }
}