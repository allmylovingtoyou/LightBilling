using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Requests;
using Api.Responses;
using Api.User;
using AutoMapper;
using Db;
using Domain;
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

        public Task<PageResponse<UserDto>> GetPage(PageRequest request)
        {
            using (var db = new ApplicationContext())
            {
                var total = db.Users.Count();
                var dbResult = db.Users.Skip(request.Skip).Take(request.Limit);

                var result = new PageResponse<UserDto>
                {
                    Data = _mapper.Map<List<UserDto>>(dbResult),
                    Total = total
                };

                return Task.FromResult(result);
            }
        }

        public async Task<UserDto> CreateUser(UserDto request)
        {
            var domain = _mapper.Map<User>(request);

            using (var db = new ApplicationContext())
            {
                var result = await db.Users.AddAsync(domain);
                await db.SaveChangesAsync();

                return _mapper.Map<UserDto>(result.Entity);
            }
        }
    }
}