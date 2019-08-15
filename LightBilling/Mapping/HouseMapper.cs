using System.Collections.Generic;
using System.Linq;
using Api.House;
using AutoMapper;
using Domain.House;
using LightBilling.Mapping.Base;

namespace LightBilling.Mapping
{
    public class HouseMapper : AbstractBaseMapper<House, HouseDto>
    {
        private readonly IMapper _mapper;

        public HouseMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }
        
        public HouseInfoDto ToInfoDto(House entity)
        {
            return _mapper.Map<HouseInfoDto>(entity);
        }
        
        public List<HouseInfoDto> ToInfoDto(IEnumerable<House> entities)
        {
            return entities.Select(ToInfoDto)
                .ToList();
        }
    }
}