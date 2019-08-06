using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Db;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LightBilling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var db = new ApplicationDbContext())
            {
                db.Database.Migrate();
            }
            
            DevelopData.Tools.Truncate();
            var houses = DevelopData.Entity.HousesDevData.Create();
            var subnets = DevelopData.Entity.NetworkDevData.Create();
            var tariffs = DevelopData.Entity.TariffDevData.Create();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => { options.Listen(IPAddress.Any, 5000); })
                .UseStartup<Startup>();
    }
}