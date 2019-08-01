using System.Threading.Tasks;
using Api.Client;
using Api.House;
using Api.Requests;
using Api.Responses;
using Db;
using Domain.Client;
using LightBilling.Extensions;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientMapper _mapper;

        public ClientService(ClientMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ClientDto> ById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Clients.FindAsync(id);

                if (result == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }

                return _mapper.ToDto(result);
            }
        }

        /// <inheritdoc />
        public async Task<ClientDto> Create(ClientDto request)
        {
            var domain = _mapper.ToEntity(request);

            using (var db = new ApplicationDbContext())
            {
                var result = await db.Clients.AddAsync(domain);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        public Task<PageResponse<ClientDto>> GetPage(PageRequest<ClientFilter> request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientDto> Update(ClientUpdateDto request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientDto> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}