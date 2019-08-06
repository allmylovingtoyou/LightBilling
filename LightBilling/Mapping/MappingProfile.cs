using System.Linq;
using Api.Client;
using Api.House;
using Api.Network;
using Api.Tariff;
using Api.User;
using AutoMapper;
using Domain;
using Domain.Client;
using Domain.House;
using Domain.Network;
using Domain.Tariff;
using Domain.User;

namespace LightBilling.Mapping
{
    /// <summary>
    /// Настройка автомапера для маппингов элементарных сущностей.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SystemUser, SystemUserDto>();
            CreateMap<SystemUserDto, SystemUser>();

            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();
//                .ForMember(x => x.Clients, opt => opt.Ignore());

            CreateMap<Subnet, SubnetDto>();
            CreateMap<SubnetDto, Subnet>()
                .ForMember(x => x.Houses, opt => opt.Ignore());
            CreateMap<Subnet, SubnetInfoDto>();

            CreateMap<Tariff, TariffDto>();
            CreateMap<TariffDto, Tariff>()
                .ForMember(x => x.JoinClients, opt => opt.Ignore());

            CreateMap<Client, ClientDto>()
                .ForMember(x => x.Tariffs, opt => opt.MapFrom(x => x.JoinTariffs.Select(t => t.Tariff)));
            CreateMap<ClientDto, Client>()
                .ForMember(x => x.JoinTariffs, opt => opt.Ignore());

            CreateMap<GreyAddress, GreyAddressDto>();
            CreateMap<GreyAddressDto, GreyAddress>()
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.ClientId, opt => opt.Ignore());

            CreateMap<WhiteAddress, WhiteAddressDto>();
            CreateMap<WhiteAddressDto, WhiteAddress>()
                .ForMember(x => x.GrayAddress, opt => opt.Ignore())
                .ForMember(x => x.GrayAddressId, opt => opt.Ignore());
            //.ForMember(x => x.Ololo, opt => opt.Ignore());
        }
    }
}