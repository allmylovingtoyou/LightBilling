using System.Collections.Generic;
using Api.Network;

namespace Api.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class ClientUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string HwIpAddress { get; set; }
        public string HwPort { get; set; }

        public string Comment { get; set; }

        public double Credit { get; set; }

        public bool IsActive { get; set; }

        public int? HouseId { get; set; }

        public int? GreyAddressId { get; set; }

        public List<int> TariffIds { get; set; } = new List<int>();
    }
}