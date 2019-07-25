using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Responses;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface IHouseService
    {
        Task<HouseDto> Create(HouseDto request);

        Task<PageResponse<HouseDto>> GetPage(PageRequest request);

        Task<HouseDto> Update(HouseUpdateDto request);

        Task<HouseDto> Delete(int id);
    }
}