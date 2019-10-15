using System;
using AutoMapper;
using Db;
using LightBilling.Mapping.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LightBillingTests.common
{
    [SetUpFixture]
    public abstract class AbstractIntegrationTests
    {
        protected ApplicationDbContext Db;
        protected Lazy<IMapper> Mapper;

        [OneTimeSetUp]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql("Host=127.0.0.1;Port=5432;Database=lightbilling_test_db;Username=lightbilling;Password=testPass");

            var db = new ApplicationDbContext(options.Options);
            db.Database.EnsureDeleted();
            db.Database.Migrate();
            Db = db;
        }

        [SetUp]
        public virtual void Setup()
        {
            Mapper = new Lazy<IMapper>(GetAutoMapper());
        }

        [TearDown]
        public void TearDown()
        {
            Db.RemoveRange(Db.Clients);
            Db.RemoveRange(Db.Houses);
            //...
            Db.SaveChanges();
        }

        private static IMapper GetAutoMapper()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            return mappingConfig.CreateMapper();
        }
    }
}