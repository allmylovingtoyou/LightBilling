using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetworkUtils.Configs;
using NetworkUtils.Services;
using Serilog;
using static System.Console;

namespace NetworkUtils
{
    class Program
    {
        private const string ConfigFile = "appsettings.json";

        static void Main(string[] args)
        {
            if (!args.Any())
            {
                WriteUsage();
                return;
            }

            if (!Enum.TryParse(args[0].Replace("--", ""), out Args enumValue))
            {
                WriteLine("Incorrect argument");
                WriteUsage();
                return;
            }

            var configuration = BuildConfiguration();
            CreateLogger(configuration);
            var serviceCollection = ConfigureServices(configuration);
            var serviceProvider = BuildServiceProvider(serviceCollection);
            var logger = serviceProvider.GetService<ILogger<Program>>();
            logger.LogInformation("#Ready for start");

            switch (enumValue)
            {
                case Args.dhcp:
                    new DhcpConfigService().Generate();
                    break;
                case Args.nat:
                    serviceProvider.GetService<NatConfigService>().Generate();
                    break;
                case Args.shaper:
                    serviceProvider.GetService<ShaperConfigService>().Generate();
                    break;
                default:
                    WriteLine("Unknown Error");
                    WriteLine();
                    return;
            }

            WriteLine();
        }

        private static void WriteUsage()
        {
            WriteLine("Usage:");
            WriteLine($"--{Args.dhcp.ToString()} - generate dhcp config");
            WriteLine($"--{Args.nat.ToString()} - generate nat config");
            WriteLine($"--{Args.shaper.ToString()} - generate shaper config");
            WriteLine();
        }

        private static IConfiguration BuildConfiguration()
        {
            Write("Building configuration");
            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile(ConfigFile)
                .Build();
            WriteLine(" OK");
            return configuration;
        }

        private static void CreateLogger(IConfiguration configuration)
        {
            Write("#Creating logger");
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .CreateLogger();
            WriteLine(" OK");
        }

        private static ServiceCollection ConfigureServices(IConfiguration configuration)
        {
            Write("#Configure services");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            WriteLine(" OK");
            return serviceCollection;
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.Configure<RouterConfig>(configuration.GetSection(RouterConfig.SectionName));
            services.AddOptions();
            services.AddTransient<NatConfigService>();
            services.AddTransient<ShaperConfigService>();
            services.AddLogging(configure => configure.AddSerilog());
        }

        private static ServiceProvider BuildServiceProvider(ServiceCollection serviceCollection)
        {
            Write("#Build ServiceProvider");
            var serviceProvider = serviceCollection.BuildServiceProvider();
            WriteLine(" OK");
            return serviceProvider;
        }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Args
    {
        dhcp,
        nat,
        shaper
    }
}