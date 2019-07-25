using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Requests;
using Api.Responses;
using Api.User;
using AutoMapper;
using Db;
using Domain.User;
using LightBilling.Interfaces;

namespace LightBilling.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<PageResponse<SystemUserDto>> GetPage(PageRequest request)
        {
            using (var db = new ApplicationDbContext())
            {
                var total = db.SystemUsers.Count();
                var dbResult = db.SystemUsers.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<SystemUserDto>
                {
                    Data = _mapper.Map<List<SystemUserDto>>(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        public async Task<SystemUserDto> CreateUser(SystemUserDto request)
        {
            var domain = _mapper.Map<SystemUser>(request);

            using (var db = new ApplicationDbContext())
            {
                var result = await db.SystemUsers.AddAsync(domain);
                await db.SaveChangesAsync();

                return _mapper.Map<SystemUserDto>(result.Entity);
            }
        }
    }
}