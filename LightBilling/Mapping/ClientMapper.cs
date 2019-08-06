using System.Collections.Generic;
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

        public List<ClientInfoDto> ToPageDto(IEnumerable<Client> entities)
        {
            return new List<ClientInfoDto>();
        }
    }
}