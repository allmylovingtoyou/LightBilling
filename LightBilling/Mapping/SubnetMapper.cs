using System.Collections.Generic;
using System.Linq;
using Api.Client;
using Api.House;
using Api.Network;
using AutoMapper;
using Domain.House;
using Domain.Network;

namespace LightBilling.Mapping
{
    public class SubnetMapper : AbstractBaseMapper<Subnet, SubnetDto>
    {
        private readonly IMapper _mapper;

        public SubnetMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }
        
        public SubnetInfoDto ToInfoDto(Subnet entity)
        {
            return _mapper.Map<SubnetInfoDto>(entity);
        }
        
        public List<SubnetInfoDto> ToInfoDto(IEnumerable<Subnet> entities)
        {
            return entities.Select(ToInfoDto)
                .ToList();
        }
    }
}