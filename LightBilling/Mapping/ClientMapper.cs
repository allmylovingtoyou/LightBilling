using System.Collections.Generic;
using System.Linq;
using Api.Client;
using AutoMapper;
using Domain.Client;
using LightBilling.Mapping.Base;

namespace LightBilling.Mapping
{
    public class ClientMapper : AbstractBaseMapper<Client, ClientDto>
    {
        private readonly IMapper _mapper;

        public ClientMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }

        public ClientInfoDto ToInfoDto(Client entity)
        {
            return _mapper.Map<ClientInfoDto>(entity);
        }
        
        public List<ClientInfoDto> ToInfoDto(IEnumerable<Client> entities)
        {
            return entities.Select(ToInfoDto)
                .ToList();
        }
    }
}