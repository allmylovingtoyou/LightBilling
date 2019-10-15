using Domain.User;
using LightBillingTests.common;
using NUnit.Framework;

namespace LightBillingTests.Services
{
    [TestFixture]
    public class UserServiceTests : AbstractIntegrationTests
    {
        [Test]
        public void DbSampleTest()
        {
            var domain = new SystemUser {Login = "login"};

            var result = Db.SystemUsers.Add(domain);
            Db.SaveChanges();

            var fromDb = Db.SystemUsers.Find(result.Entity.Id);

            Assert.That(domain.Login, Is.EqualTo(fromDb.Login));
        }
    }
}