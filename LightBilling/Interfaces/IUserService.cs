using System.Threading.Tasks;
using Api.Requests;
using Api.Responses;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface IUserService
    {
        Task<SystemUserDto> CreateUser(SystemUserDto request);

        Task<PageResponse<SystemUserDto>> GetPage(PageRequest request);
    }
}