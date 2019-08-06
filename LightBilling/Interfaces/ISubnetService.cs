using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Network;
using Api.Requests;
using Api.Responses;
using Api.User;

namespace LightBilling.Interfaces
{
    public interface ISubnetService
    {
        Task<List<SubnetInfoDto>> GetFreeSubnets();

        Task<List<SubnetInfoDto>> GetAllSubnets();
    }
}