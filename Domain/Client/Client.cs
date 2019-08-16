using System.Collections.Generic;
using Domain.Base;
using Domain.Network;
using Domain.Tariff;

namespace Domain.Client
{
    /// <summary>
    /// Сущность абонента. Стринги в большинстве полей, т.к. это Лайт биллинг)
    /// </summary>
    public class Client : IBaseEntity
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

        public double Balance { get; set; }

        public double Credit { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<JoinClientsTariffs> JoinTariffs { get; set; }

        public int? HouseId { get; set; }
        public virtual House.House House { get; set; }

        public int? GreyAddressId { get; set; }

        public virtual GreyAddress GreyAddress { get; set; }
    }
}