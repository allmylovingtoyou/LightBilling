using Api.House;
using Api.User;
using AutoMapper;
using Domain;
using Domain.House;
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
            CreateMap<HouseDto, House>()
                .ForMember(x => x.Clients, opt => opt.Ignore());

            //.ForMember(x => x.Ololo, opt => opt.Ignore());
        }

    }

}