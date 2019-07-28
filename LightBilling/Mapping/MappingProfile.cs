using Api.House;
using Api.Network;
using Api.Tariff;
using Api.User;
using AutoMapper;
using Domain;
using Domain.House;
using Domain.Network;
using Domain.Tariff;
using Domain.User;

namespace LightBilling.Mapping {

    /// <summary>
    /// Настройка автомапера для маппингов элементарных сущностей.
    /// </summary>
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<SystemUser, SystemUserDto>();
            CreateMap<SystemUserDto, SystemUser>();

            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();
//                .ForMember(x => x.Clients, opt => opt.Ignore());

            CreateMap<Subnet, SubnetDto>();
            CreateMap<SubnetDto, Subnet>()
                .ForMember(x => x.Houses, opt => opt.Ignore());
            
            
            CreateMap<Tariff, TariffDto>();
            CreateMap<TariffDto, Tariff>();
//                .ForMember(x => x.HouseId, opt => opt.Ignore());

            //.ForMember(x => x.Ololo, opt => opt.Ignore());
        }

    }

}