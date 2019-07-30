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
        Task<List<SubnetDto>> GetFreeSubnets();

        Task<List<SubnetDto>> GetAllSubnets();
    }
}