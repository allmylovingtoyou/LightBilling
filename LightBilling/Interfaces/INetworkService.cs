using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Network;
using Api.Requests;
using Api.Responses;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface INetworkService
    {
        Task<List<SubnetInfoDto>> GetFreeSubnets();

        Task<List<SubnetInfoDto>> GetAllSubnets();
        
        Task<List<GreyAddressDto>> GetFreeAddressesByHouseId(int houseId);

        Task<List<WhiteAddressDto>> GetFreeWhiteAddresses();
    }
}