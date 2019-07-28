using System.Threading.Tasks;
using Api.House;
using Api.Requests;
using Api.Responses;
using Api.Tariff;

namespace LightBilling.Interfaces
{
    public interface ITariffService
    {

        Task<TariffDto> ById(int id);
        Task<TariffDto> Create(TariffDto request);

        Task<PageResponse<TariffDto>> GetPage(PageRequest<TariffFilter> request);

        Task<TariffDto> Update(TariffUpdateDto request);

        Task<TariffDto> Delete(int id);
    }
}