using System.Threading.Tasks;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto request);
    }
}