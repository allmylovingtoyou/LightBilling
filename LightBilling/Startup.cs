using AutoMapper;
using Db;
using LightBilling.Interfaces;
using LightBilling.Mapping;
using LightBilling.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LightBilling
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var autoMapper = CreateAutoMapper();

            services.AddSingleton(autoMapper);

            services.AddScoped<ISystemUserService, SystemUserService>();
            
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<HouseMapper>();
            
            services.AddScoped<ITariffService, TariffService>();
            services.AddScoped<TariffMapper>();
            
            services.AddScoped<ISubnetService, SubnetService>();
            services.AddScoped<SubnetMapper>();
            
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ClientMapper>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMapper autoMapper)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseMvc();
        }
        
        private static IMapper CreateAutoMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            var autoMapper = mapperConfiguration.CreateMapper();
            autoMapper.ConfigurationProvider.AssertConfigurationIsValid();
            return autoMapper;
        }
    }
}