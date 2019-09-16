using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Client;
using Api.House;
using Api.Requests;
using Api.Requests.Extensions;
using Api.Responses;
using Castle.Core.Internal;
using Db;
using Domain.Client;
using Domain.Tariff;
using LightBilling.Extensions;
using LightBilling.Interfaces;
using LightBilling.Mapping;
using LightBilling.Repositories;

namespace LightBilling.Services
{
    public class ClientService : IClientService
    {
        private readonly ClientMapper _mapper;
        private readonly ClientRepository _repository;

        public ClientService(ClientMapper mapper, ClientRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
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
            using (var db = new ApplicationDbContext())
            {
                var client = _mapper.ToEntity(request);

                CreateTariffs(request, client);
                await AddNetworkAddresses(request, db);

                var result = await db.Clients.AddAsync(client);
                await db.SaveChangesAsync();

                return await ById(result.Entity.Id);
            }
        }

        private static async Task AddNetworkAddresses(ClientDto request, ApplicationDbContext db)
        {
            if (request.WhiteAddressId.HasValue)
            {
                var gray = await db.GreyAddresses.FindAsync(request.GreyAddressId);
                if (gray.White == null || gray.White.Id != request.WhiteAddressId)
                {
                    var white = db.WhiteAddresses.Find(request.WhiteAddressId.Value);
                    gray.White = white;
                }
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
                var r = dbResult.Skip(request.Skip).Take(request.Limit).ToList();

                var result = new PageResponse<ClientInfoDto>
                {
                    Data = _mapper.ToInfoDto(r),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        public async Task<ClientDto> Update(ClientUpdateDto request)
        {
            //TODO need implement validator

            using (var db = new ApplicationDbContext())
            {
                var client = db.Clients
                    .AsQueryable();

                var toUpdate = client.FirstOrDefault(h => h.Id == request.Id);

                if (toUpdate == null)
                {
                    throw new InternalExceptions.NotFoundException($"{nameof(Client)} with id  {request.Id}");
                }

                toUpdate.FullName = request.FullName;
                toUpdate.PassportData = request.PassportData;
                toUpdate.PhoneNumber = request.PhoneNumber;
                toUpdate.Login = request.Login;
                toUpdate.Password = request.Password;
                toUpdate.HwIpAddress = request.HwIpAddress;
                toUpdate.HwPort = request.HwPort;
                toUpdate.Comment = request.Comment;
                toUpdate.Credit = request.Credit;
                toUpdate.IsActive = request.IsActive;
                toUpdate.HouseId = request.HouseId;
                toUpdate.ApartmentNumber = request.ApartmentNumber;
                toUpdate.CreditValidFrom = request.CreditValidFrom;
                toUpdate.CreditValidTo = request.CreditValidTo;
                toUpdate.MacAddress = request.MacAddress;

                await GreyAddressUpdate(request, db, toUpdate);
                UpdateTariffs(request, toUpdate);
                await WhiteAddressUpdate(request, db, toUpdate);

                db.Clients.Update(toUpdate);

                await db.SaveChangesAsync();
                return await ById(toUpdate.Id);
            }
        }

        public async Task<ClientDto> Delete(int id)
        {
            using (var db = new ApplicationDbContext())
            {
                var client = await db.Clients.FindAsync(id);

                if (client == null)
                {
                    throw new InternalExceptions.NotFoundException($"{nameof(Client)} with id  {id}");
                }

                client.IsDeleted = true;
                db.Clients.Update(client);
                await db.SaveChangesAsync();

                return _mapper.ToDto(client);
            }
        }

        public async Task<double> UpdateBalance(double newBalance, int clientId)
        {
            using (var db = new ApplicationDbContext())
            {
                var client = await db.Clients.FindAsync(clientId);
                client.Balance = newBalance;
                db.Clients.Update(client);
                await db.SaveChangesAsync();
                return client.Balance;
            }
        }

        private static async Task GreyAddressUpdate(ClientUpdateDto request, ApplicationDbContext db, Client toUpdate)
        {
            if (request.GreyAddressId.HasValue)
            {
                if (toUpdate.GreyAddress != null && toUpdate.GreyAddressId != request.GreyAddressId.Value && toUpdate.GreyAddress.White != null)
                {
                    var grey = await db.GreyAddresses.FindAsync(toUpdate.GreyAddressId);
                    if (grey.White != null)
                    {
                        grey.White.GrayAddress = null;
                    }
                }
            }

            toUpdate.GreyAddressId = request.GreyAddressId;
        }

        private static async Task WhiteAddressUpdate(ClientUpdateDto request, ApplicationDbContext db, Client toUpdate)
        {
            if (request.WhiteAddressId.HasValue)
            {
                var grey = await db.GreyAddresses.FindAsync(toUpdate.GreyAddressId);
                if (grey.White?.Id != request.WhiteAddressId)
                {
                    var white = await db.WhiteAddresses.FindAsync(request.WhiteAddressId);
                    grey.White = white;
                }
            }
            else
            {
                if (toUpdate.GreyAddressId != null)
                {
                    var grey = await db.GreyAddresses.FindAsync(toUpdate.GreyAddressId);
                    if (grey.White != null)
                    {
                        grey.White.GrayAddress = null;
                    }
                }
            }
        }

        //TODO refactor this after tests writing
        private static void UpdateTariffs(ClientUpdateDto request, Client toUpdate)
        {
            var currentTariffIds = toUpdate.JoinTariffs
                .Select(x => x.TariffId)
                .ToList();

            var toAdd = request.TariffIds.Except(currentTariffIds)
                .ToList();

            var toDel = currentTariffIds.Except(request.TariffIds)
                .ToList();
            foreach (var i in toDel)
            {
                toUpdate.JoinTariffs = toUpdate.JoinTariffs.Where(x => x.TariffId != i)
                    .ToList();
            }

            foreach (var i in toAdd)
            {
                toUpdate.JoinTariffs.Add(new JoinClientsTariffs
                {
                    ClientId = toUpdate.Id,
                    TariffId = i
                });
            }
        }

        private static void CreateTariffs(ClientDto request, Client client)
        {
            if (!request.TariffIds.IsNullOrEmpty())
            {
                var joins = client.JoinTariffs = new List<JoinClientsTariffs>();
                foreach (var tariffId in request.TariffIds)
                {
                    joins.Add(new JoinClientsTariffs {Client = client, TariffId = tariffId});
                }
            }
        }

        private static IQueryable<Client> Filter(PageRequest<ClientFilter> request, IQueryable<Client> dbResultMain)
        {
            var filter = request.Filter;
            if (filter != null)
            {
                if (filter.Name != null)
                {
                    dbResultMain = dbResultMain.Where(x => x.FullName.Contains(filter.Name));
                }
            }

            dbResultMain = dbResultMain.Where(x => !x.IsDeleted);
            return dbResultMain;
        }

        private static IQueryable<Client> Sort(PageRequest<ClientFilter> request, IQueryable<Client> dbResult)
        {
            var sort = request.Sort;
            if (sort?.FieldName == null)
            {
                return dbResult;
            }

            if (sort.FieldName.Equals(nameof(Client.FullName).ToLowerInvariant()))
            {
                dbResult = sort.Order == SortType.Asc
                    ? dbResult.OrderBy(x => x.FullName)
                    : dbResult.OrderByDescending(x => x.FullName);
            }

            return dbResult;
        }
    }
}