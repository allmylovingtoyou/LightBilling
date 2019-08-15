using System.Threading.Tasks;
using Api.Client;
using Api.House;
using Api.Requests;
using Api.Responses;

namespace LightBilling.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto> ById(int id);

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ClientDto> Create(ClientDto request);

        Task<PageResponse<ClientInfoDto>> GetPage(PageRequest<ClientFilter> request);

        Task<ClientDto> Update(ClientUpdateDto request);

        Task<ClientDto> Delete(int id);

        Task<double> UpdateBalance(double newBalance, int clientId);
    }
}