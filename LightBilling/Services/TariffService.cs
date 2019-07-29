using System.Linq;
using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Requests.Extensions;
using Api.Responses;
using Api.Tariff;
using Db;
using Domain.House;
using Domain.Tariff;
using LightBilling.Extensions;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class TariffService : ITariffService
    {
        private readonly TariffMapper _mapper;

        public TariffService(TariffMapper mapper)
        {
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<TariffDto> ById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Tariffs.FindAsync(id);

                if (result == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }

                return _mapper.ToDto(result);
            }
        }

        /// <inheritdoc />
        public Task<PageResponse<TariffDto>> GetPage(PageRequest<TariffFilter> request)
        {
            using (var db = new ApplicationDbContext())
            {
                var dbResult = db.Tariffs.AsQueryable();

                dbResult = Filter(request, dbResult);

                dbResult = Sort(request, dbResult);

                var total = dbResult.Count();
                dbResult = dbResult.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<TariffDto>
                {
                    Data = _mapper.ToDto(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        /// <inheritdoc />
        public async Task<TariffDto> Create(TariffDto request)
        {
            var domain = _mapper.ToEntity(request);

            using (var db = new ApplicationDbContext())
            {
                var result = await db.Tariffs.AddAsync(domain);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        /// <inheritdoc />
        public async Task<TariffDto> Update(TariffUpdateDto request)
        {
            //TODO need implement validator

            using (var db = new ApplicationDbContext())
            {
                var toUpdate = await db.Tariffs.FindAsync(request.Id);

                if (toUpdate == null)
                {
                    throw new InternalExceptions.NotFoundException(request.Id.ToString());
                }

                toUpdate.Name = request.Name;
                toUpdate.Comment = request.Comment;
                toUpdate.IsPeriodic = request.IsPeriodic;
                toUpdate.Type = (TariffType)request.Type;
                toUpdate.InputRate = request.InputRate;
                toUpdate.OutputRate = request.OutputRate;

                var result = db.Tariffs.Update(toUpdate);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        /// <inheritdoc />
        public async Task<TariffDto> Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var toDelete = await db.Tariffs.FindAsync(id);

                if (toDelete == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }

                var result = db.Tariffs.Remove(toDelete);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        private static IQueryable<Tariff> Filter(PageRequest<TariffFilter> request, IQueryable<Tariff> dbResultMain)
        {
            var filter = request.Filter;
            if (filter != null)
            {
                if (filter.Name != null)
                {
                    dbResultMain = dbResultMain.Where(x => x.Name.Contains(filter.Name));
                }
                
                if (filter.Type.HasValue)
                {
                    dbResultMain = dbResultMain.Where(x => x.Type.Equals(filter.Type.Value));
                }
            }

            return dbResultMain;
        }

        private static IQueryable<Tariff> Sort(PageRequest<TariffFilter> request, IQueryable<Tariff> dbResult)
        {
            var sort = request.Sort;

            if (sort?.FieldName == null)
            {
                return dbResult;
            }

            if (sort.FieldName.Equals(nameof(TariffDto.Name).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Name)
                    : dbResult.OrderByDescending(x => x.Name);
            }

            return dbResult;
        }
    }
}