﻿using System;
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
using Serilog;

namespace LightBilling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
//            using (var db = new ApplicationDbContext())
//            {
//                db.Database.EnsureDeleted();
//                db.Database.Migrate();
//            }
//            
//            DevelopData.Tools.Truncate();
//            DevelopData.Entity.NetworkDevData.Create();
//            DevelopData.Entity.HousesDevData.Create();
//            DevelopData.Entity.TariffDevData.Create();
//            DevelopData.Entity.ClientDevData.Create();

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration))
                .UseKestrel(options => { options.Listen(IPAddress.Any, 5000); })
                .UseStartup<Startup>();
    }
}