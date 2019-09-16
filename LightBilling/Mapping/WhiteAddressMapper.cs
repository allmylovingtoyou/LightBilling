using Api.Network;
using AutoMapper;
using Domain.Network;
using LightBilling.Mapping.Base;

namespace LightBilling.Mapping
{
    public class WhiteAddressMapper : AbstractBaseMapper<WhiteAddress, WhiteAddressDto>
    {
        private readonly IMapper _mapper;

        public WhiteAddressMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }
    }
}