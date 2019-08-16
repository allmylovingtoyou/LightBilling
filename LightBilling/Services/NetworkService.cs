using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Network;
using Api.Requests;
using Api.Responses;
using Api.User;
using AutoMapper;
using Castle.Core.Internal;
using Db;
using Domain.User;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    public class NetworkService : INetworkService
    {
        private readonly SubnetMapper _subnetMapper;
        private readonly GreyAddressMapper _greyAddressMapper;

        public NetworkService(SubnetMapper subnetMapper, GreyAddressMapper greyAddressMapper)
        {
            _subnetMapper = subnetMapper;
            _greyAddressMapper = greyAddressMapper;
        }

        public Task<List<SubnetInfoDto>> GetFreeSubnets()
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Subnets.Where(x => !x.Houses.Any());

                return Task.FromResult(_subnetMapper.ToInfoDto(result));
            }
        }

        public Task<List<SubnetInfoDto>> GetAllSubnets()
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Subnets;

                return Task.FromResult(_subnetMapper.ToInfoDto(result));
            }
        }

        public async Task<List<GreyAddressDto>> GetFreeAddressesByHouseId(int houseId)
        {
            using (var db = new ApplicationDbContext())
            {
                var result = await db.Houses.FindAsync(houseId);

                if (result?.Subnet?.Addresses == null)
                {
                    return new List<GreyAddressDto>();
                }

                var addresses = result.Subnet.Addresses.Where(address => !address.ClientId.HasValue);

                return _greyAddressMapper.ToDto(addresses);
            }
        }
    }
}