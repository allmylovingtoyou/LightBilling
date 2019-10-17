using System;
using System.Collections.Generic;
using System.Linq;
using Api.Client;
using Api.Client.Status;
using AutoMapper;
using Domain.Client;
using LightBilling.Mapping.Base;

namespace LightBilling.Mapping
{
    public class ClientMapper : AbstractBaseMapper<Client, ClientDto>
    {
        private readonly IMapper _mapper;

        public ClientMapper(IMapper mapper) : base(mapper)
        {
            _mapper = mapper;
        }

        public override ClientDto ToDto(Client entity)
        {
            var dto = _mapper.Map<ClientDto>(entity);
            dto.Status = CalculateClientStatus(entity);
            return dto;
        }

        public ClientInfoDto ToInfoDto(Client entity)
        {
            var dto = _mapper.Map<ClientInfoDto>(entity);
            dto.Status = CalculateClientStatus(entity);
            return dto;
        }

        public List<ClientInfoDto> ToInfoDto(IEnumerable<Client> entities)
        {
            return entities.Select(ToInfoDto)
                .ToList();
        }

        public static ClientStatus CalculateClientStatus(Client client)
        {
            if (!client.IsActive)
            {
                return ClientStatus.NotActive;
            }

            double amount = 0;
            amount = client.CreditValidFrom.HasValue && client.CreditValidTo.HasValue
                ? GetByCredit(client.CreditValidFrom.Value, client.CreditValidTo.Value, client.Balance, client.Credit)
                : client.Balance;

            return amount < 0 
                ? ClientStatus.NegativeBalance 
                : ClientStatus.Active;
        }

        private static double GetByCredit(DateTime from, DateTime to, double balance, double credit)
        {
            double amount;
            amount = (from <= DateTime.Now && to >= DateTime.Now)
                ? amount = balance + credit
                : balance;
            return amount;
        }
    }
}