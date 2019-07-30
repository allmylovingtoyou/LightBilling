using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Network;
using Api.Requests;
using Api.Responses;
using Api.User;
using AutoMapper;
using Db;
using Domain.User;
using LightBilling.Interfaces;
using LightBilling.Mapping;

namespace LightBilling.Services
{
    public class SubnetService : ISubnetService {
        
    private readonly SubnetMapper _mapper;

        public SubnetService(SubnetMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<List<SubnetDto>> GetFreeSubnets()
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Subnets.Where(x => !x.Houses.Any());

                return Task.FromResult(_mapper.ToDto(result));
            }
        }
        
        public Task<List<SubnetDto>> GetAllSubnets()
        {
            using (var db = new ApplicationDbContext())
            {
                var result = db.Subnets;

                return Task.FromResult(_mapper.ToDto(result));
            }
        }
    }
}