using Api.House;
using AutoMapper;
using Domain.House;

namespace LightBilling.Mapping
{
    public class HouseMapper : AbstractBaseMapper<House, HouseDto>
    {
        public HouseMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}