using System.Diagnostics;
using System.Threading.Tasks;
using Api.Client;
using LightBilling.Mapping;
using LightBilling.Repositories;
using LightBilling.Services;
using LightBillingTests.common;
using NUnit.Framework;

namespace LightBillingTests.Services
{
    [TestFixture]
    public class ClientServiceTests : AbstractIntegrationTests
    {
        private ClientService _service;
        private ClientRepository _repository;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            _repository = new ClientRepository();
            _service = new ClientService(new ClientMapper(Mapper.Value), _repository);
        }
        
        [Test]
        public async Task Create()
        {
            var client = new ClientDto
            {
                FullName = "fullName"
            };

            var result = await _service.Create(client);

            Debug.Assert(result.Id != null, "result.Id != null");
            var fromDb = await _repository.ById(result.Id.Value);

            Assert.That(client.FullName, Is.EqualTo(fromDb.FullName));
        }
    }
}