using System.Threading.Tasks;
using Api.Requests;
using Api.Responses;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(UserDto request);

        Task<PageResponse<UserDto>> GetPage(PageRequest request);
    }
}