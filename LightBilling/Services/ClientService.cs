using System.Linq;
using System.Threading.Tasks;
using Api.Client;
using Api.House;
using Api.Requests;
using Api.Requests.Extensions;
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

        public Task<PageResponse<ClientInfoDto>> GetPage(PageRequest<ClientFilter> request)
        {
            using (var db = new ApplicationDbContext())
            {
                var dbResult = db.Clients.AsQueryable();

                dbResult = Filter(request, dbResult);

                dbResult = Sort(request, dbResult);


                var total = dbResult.Count();
                dbResult = dbResult.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<ClientInfoDto>
                {
                    Data = _mapper.ToPageDto(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        public Task<ClientDto> Update(ClientUpdateDto request)
        {
            throw new System.NotImplementedException();
        }

        public Task<ClientDto> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
        
        private static IQueryable<Client> Filter(PageRequest<ClientFilter> request, IQueryable<Client> dbResultMain)
        {
            var filter = request.Filter;
            if (filter != null)
            {
                if (filter.Name != null)
                {
                    dbResultMain = dbResultMain.Where(x => x.Name.Contains(filter.Name));
                }
            }

            return dbResultMain;
        }

        private static IQueryable<Client> Sort(PageRequest<ClientFilter> request, IQueryable<Client> dbResult)
        {
            var sort = request.Sort;

            if (sort?.FieldName == null)
            {
                return dbResult;
            }

            if (sort.FieldName.Equals(nameof(Client.Name).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Name)
                    : dbResult.OrderByDescending(x => x.Name);
            }

            return dbResult;
        }
        
    }
}