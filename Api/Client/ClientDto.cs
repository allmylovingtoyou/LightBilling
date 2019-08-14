using System.Collections.Generic;
using Api.House;
using Api.Network;
using Api.Tariff;

namespace Api.Client
{
    public class ClientDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string HwIpAddress { get; set; }
        public string HwPort { get; set; }

        public string Comment { get; set; }

        public double Balance { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int HouseId { get; set; }
        public HouseDto House { get; set; }

        public GreyAddressDto GreyAddress { get; set; }

        public virtual List<TariffDto> Tariffs { get; set; }
    }
}