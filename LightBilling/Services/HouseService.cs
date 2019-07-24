using System.Linq;
using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Responses;
using Db;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    public class HouseService : IHouseService
    {
        private readonly HouseMapper _mapper;

        public HouseService(HouseMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<PageResponse<HouseDto>> GetPage(PageRequest request)
        {
            using (var db = new ApplicationContext())
            {
                var total = db.Houses.Count();
                var dbResult = db.Houses.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<HouseDto>
                {
                    Data = _mapper.ToDto(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        public async Task<HouseDto> Create(HouseDto request)
        {
            var domain = _mapper.ToEntity(request);

            using (var db = new ApplicationContext())
            {
                var result = await db.Houses.AddAsync(domain);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }
    }
}