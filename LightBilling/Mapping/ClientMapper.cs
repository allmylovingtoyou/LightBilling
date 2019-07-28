using Api.House;
using AutoMapper;
using Domain.House;

namespace LightBilling.Mapping
{
    public class ClientMapper : AbstractBaseMapper<House, HouseDto>
    {
        public ClientMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}