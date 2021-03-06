using System.Linq;
using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Requests.Extensions;
using Api.Responses;
using Db;
using Domain.House;
using LightBilling.Extensions;
using LightBilling.Interfaces;
using LightBilling.Mapping;
using Microsoft.EntityFrameworkCore;

namespace LightBilling.Services
{
    /// <inheritdoc />
    public class HouseService : IHouseService
    {
        private readonly HouseMapper _mapper;
        private readonly SubnetMapper _subnetMapper;

        public HouseService(HouseMapper mapper, SubnetMapper subnetMapper)
        {
            _mapper = mapper;
            _subnetMapper = subnetMapper;
        }

        /// <inheritdoc />
        public async Task<HouseDto> ById(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Houses.FindAsync(id);

                if (result == null)
                {
                    throw new InternalExceptions.NotFoundException(id.ToString());
                }

                return _mapper.ToDto(result);
            }
        }

        /// <inheritdoc />
        public Task<PageResponse<HouseInfoDto>> GetPage(PageRequest<HouseFilter> request)
        {
            using (var db = new ApplicationDbContext())
            {
                var dbResult = db.Houses
                    .Include(h => h.Subnet)
                    .AsQueryable();

                dbResult = Filter(request, dbResult);

                dbResult = Sort(request, dbResult);

                var total = dbResult.Count();
                dbResult = dbResult.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<HouseInfoDto>
                {
                    Data = _mapper.ToInfoDto(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        /// <inheritdoc />
        public async Task<HouseDto> Create(HouseDto request)
        {
            var domain = _mapper.ToEntity(request);

            using (var db = new ApplicationDbContext())
            {
                var result = await db.Houses.AddAsync(domain);
                await db.SaveChangesAsync();

                return await ById(result.Entity.Id);
            }
        }

        /// <inheritdoc />
        public async Task<HouseDto> Update(HouseUpdateDto request)
        {
            //TODO need implement validator

            using (var db = new ApplicationDbContext())
            {
                var houses = db
                    .Houses.Include(h => h.Subnet)
                    .AsQueryable();

                var toUpdate = houses.FirstOrDefault(h => h.Id == request.Id);

                if (toUpdate == null)
                {
                    throw new InternalExceptions.NotFoundException($"house with id  {request.Id}");
                }

                toUpdate.Address = request.Address;
                toUpdate.Comment = request.Comment;
                toUpdate.Number = request.Number;
                toUpdate.AdditionalNumber = request.AdditionalNumber;
                toUpdate.SubnetId = request.SubnetId;

                var result = db.Houses.Update(toUpdate);
                await db.SaveChangesAsync();

                return _mapper.ToDto(result.Entity);
            }
        }

        /// <inheritdoc />
        public async Task<HouseDto> Delete(int id)
        {
            using (var db = new ApplicationDbContext())
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

        private static IQueryable<House> Filter(PageRequest<HouseFilter> request, IQueryable<House> dbResultMain)
        {
            var filter = request.Filter;
            if (filter == null) return dbResultMain;

            if (filter.Address != null)
            {
                dbResultMain = dbResultMain.Where(x => x.Address.Contains(filter.Address));
            }

            if (filter.Number != null)
            {
                dbResultMain = dbResultMain.Where(x => x.Number.Contains(filter.Number));
            }

            if (filter.AdditionalNumber != null)
            {
                dbResultMain = dbResultMain.Where(x => x.AdditionalNumber.Contains(filter.AdditionalNumber));
            }

            if (filter.Comment != null)
            {
                dbResultMain = dbResultMain.Where(x => x.Comment.Contains(filter.Comment));
            }

            if (filter.Composite != null)
            {
                var parts = filter.Composite.Split(", ");
                if (parts.Any())
                {
                    dbResultMain = dbResultMain.Where(x => x.Address.ToLower().Contains(parts[0].ToLower()));

                    if (parts.Length >= 2 && !string.IsNullOrWhiteSpace(parts[1]))
                    {
                        dbResultMain = dbResultMain.Where(x => x.Number.ToLower().Contains(parts[1].ToLower()));
                    }

                    if (parts.Length >= 3 && !string.IsNullOrWhiteSpace(parts[1]))
                    {
                        dbResultMain = dbResultMain.Where(x => x.AdditionalNumber.ToLower().Contains(parts[2].ToLower()));
                    }
                }
            }

            return dbResultMain;
        }

        private static IQueryable<House> Sort(PageRequest<HouseFilter> request, IQueryable<House> dbResult)
        {
            var sort = request.Sort;

            if (sort?.FieldName == null)
            {
                return dbResult;
            }

            if (sort.FieldName.Equals(nameof(House.Id).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Id)
                    : dbResult.OrderByDescending(x => x.Id);
            }

            if (sort.FieldName.Equals(nameof(House.Address).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Address)
                    : dbResult.OrderByDescending(x => x.Address);
            }

            if (sort.FieldName.Equals(nameof(House.AdditionalNumber).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.AdditionalNumber)
                    : dbResult.OrderByDescending(x => x.AdditionalNumber);
            }

            if (sort.FieldName.Equals(nameof(House.Number).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Number)
                    : dbResult.OrderByDescending(x => x.Number);
            }

            if (sort.FieldName.Equals(nameof(House.Comment).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.Comment)
                    : dbResult.OrderByDescending(x => x.Comment);
            }

            return dbResult;
        }
    }
}