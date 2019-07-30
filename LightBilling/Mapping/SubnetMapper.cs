using Api.House;
using Api.Network;
using AutoMapper;
using Domain.House;
using Domain.Network;

namespace LightBilling.Mapping
{
    public class SubnetMapper : AbstractBaseMapper<Subnet, SubnetDto>
    {
        public SubnetMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}