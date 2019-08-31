using System;
using System.Collections.Generic;
using Api.House;
using Api.Network;
using Api.Tariff;

namespace Api.Client
{
    public class ClientDto
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public string PassportData { get; set; }

        public string PhoneNumber { get; set; }

        public string HwIpAddress { get; set; }
        public string HwPort { get; set; }

        public string Comment { get; set; }

        public double Balance { get; set; }

        public double Credit { get; set; }
        public DateTime? CreditValidFrom { get; set; }
        public DateTime? CreditValidTo { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int? HouseId { get; set; }
        public HouseInfoDto House { get; set; }

        public string ApartmentNumber { get; set; }

        public int? GreyAddressId { get; set; }
        public GreyAddressDto GreyAddress { get; set; }

        public List<TariffDto> Tariffs { get; set; }

        //TODO сделать чтоб заполнялось при отдаче или сделать createDto
        public List<int> TariffIds { get; set; }
    }
}