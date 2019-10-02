using System.Linq;
using Api.Client;
using Api.House;
using Api.Network;
using Api.Tariff;
using Api.User;
using AutoMapper;
using Domain.Client;
using Domain.House;
using Domain.Network;
using Domain.Tariff;
using Domain.User;

namespace LightBilling.Mapping.Base
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
            CreateMap<HouseDto, House>()
                .ForMember(x => x.Subnet, opt => opt.Ignore());

            CreateMap<House, HouseInfoDto>()
                .ForMember(x => x.SubnetString, opt => opt.MapFrom(d => d.Subnet.Net + "/" + d.Subnet.Mask));

            CreateMap<Subnet, SubnetDto>();
            CreateMap<SubnetDto, Subnet>()
                .ForMember(x => x.Houses, opt => opt.Ignore());
            CreateMap<Subnet, SubnetInfoDto>();

            CreateMap<Tariff, TariffDto>();
            CreateMap<TariffDto, Tariff>()
                .ForMember(x => x.JoinClients, opt => opt.Ignore());
            
            CreateMap<Client, ClientDto>()
                .ForMember(x => x.Tariffs, opt => opt.MapFrom(x => x.JoinTariffs.Select(t => t.Tariff)))
                .ForMember(x => x.TariffIds, opt => opt.MapFrom(x => x.JoinTariffs.Select(t => t.TariffId)))
                .ForMember(x => x.WhiteAddressId, opt => opt.MapFrom(x => x.GreyAddress.White.Id))
                .ForMember(x => x.WhiteAddress, opt => opt.MapFrom(x => x.GreyAddress.White))
                .ForMember(x => x.Status, opt => opt.Ignore());

            CreateMap<ClientDto, Client>()
                .ForMember(x => x.JoinTariffs, opt => opt.Ignore())
                .ForMember(x => x.House, opt => opt.Ignore());

            CreateMap<Client, ClientInfoDto>()
                .ForMember(x => x.Status, opt => opt.Ignore());

            CreateMap<GreyAddress, GreyAddressDto>();
            CreateMap<GreyAddressDto, GreyAddress>()
                .ForMember(x => x.Client, opt => opt.Ignore())
                .ForMember(x => x.ClientId, opt => opt.Ignore());

            CreateMap<WhiteAddress, WhiteAddressDto>();
            CreateMap<WhiteAddressDto, WhiteAddress>()
                .ForMember(x => x.GrayAddress, opt => opt.Ignore());
//                .ForMember(x => x.GrayAddressId, opt => opt.Ignore());
        }
    }
}