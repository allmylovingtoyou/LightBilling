using Api.Client;
using AutoMapper;
using Domain.Client;

namespace LightBilling.Mapping
{
    public class ClientMapper : AbstractBaseMapper<Client, ClientDto>
    {
        public ClientMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}