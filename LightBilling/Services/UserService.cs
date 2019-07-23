using System.Threading.Tasks;
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