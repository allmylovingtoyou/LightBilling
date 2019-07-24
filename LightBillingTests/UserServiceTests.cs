using System;
using System.Linq;
using Api.User;
using Db;
using Domain.User;
using Xunit;

namespace LightBillingTests
{
    public class UnitTest1
    {
        [Fact]
        public void DbSampleTest()
        {
            using (var db = new ApplicationContext())
            {
                var domain = new SystemUser {Login = "login"};

                var result = db.SystemUsers.Add(domain);
                db.SaveChanges();

                var fromDb = db.SystemUsers.Find(result.Entity.Id);

                Assert.Equal(domain.Login, fromDb.Login);
            }
        }
    }
}