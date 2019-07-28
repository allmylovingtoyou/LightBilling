using Api.House;
using Api.Network;

namespace Api.Client
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
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

        public int FullAddressId { get; set; }
        public FullAddressDto FullNetworkAddress { get; set; }
    }
}