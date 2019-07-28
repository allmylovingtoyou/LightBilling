using Api.House;
using Api.Tariff;
using AutoMapper;
using Domain.House;
using Domain.Tariff;

namespace LightBilling.Mapping
{
    public class TariffMapper : AbstractBaseMapper<Tariff, TariffDto>
    {
        public TariffMapper(IMapper mapper) : base(mapper)
        {
        }
    }
}