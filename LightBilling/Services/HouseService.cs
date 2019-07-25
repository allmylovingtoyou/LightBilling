using System.Linq;
using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Responses;
using Db;
using LightBilling.Extensions;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class HouseService : IHouseService
    {
        private readonly HouseMapper _mapper;

        public HouseService(HouseMapper mapper)
        {
            _mapper = mapper;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
        
        /// <inheritdoc />
        public async Task<HouseDto> Update(HouseUpdateDto request)
        {
            //TODO need implement validator
            
           using (var db = new ApplicationContext())
           {
               var toUpdate = await db.Houses.FindAsync(request.Id);

               if (toUpdate == null)
               {
                   throw new InternalExceptions.NotFoundException(request.Id.ToString());
               }

               toUpdate.Address = request.Address;
               toUpdate.Comment = request.Comment;
               toUpdate.Number = request.Number;
               toUpdate.AdditionalNumber = request.AdditionalNumber;

                var result = db.Houses.Update(toUpdate);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        /// <inheritdoc />
        public async Task<HouseDto> Delete(int id)
        {
            using (var db = new ApplicationContext())
            {
                var toDelete = await db.Houses.FindAsync(id);
                
                if (toDelete == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }
                
                var result = db.Houses.Remove(toDelete);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }
    }
}