using Api.User;
using AutoMapper;
using Domain;
using Domain.User;

namespace LightBilling.Mapping {

    /// <summary>
    /// Настройка автомапера для маппингов элементарных сущностей.
    /// </summary>
    public class MappingProfile : Profile {

        public MappingProfile() {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            
            //.ForMember(x => x.Ololo, opt => opt.Ignore());
        }

    }

}