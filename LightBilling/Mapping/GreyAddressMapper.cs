using System.Collections.Generic;
using System.Linq;
using Api.Client;
using Api.House;
using Api.Network;
using AutoMapper;
using Domain.House;
using Domain.Network;
using LightBilling.Mapping.Base;

namespace LightBilling.Mapping
{
    public class GreyAddressMapper : AbstractBaseMapper<GreyAddress, GreyAddressDto>
    {
        private readonly IMapper _mapper;

        public GreyAddressMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }
    }
}