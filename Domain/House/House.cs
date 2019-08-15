using System.Collections.Generic;
using Domain.Base;
using Domain.Network;

namespace Domain.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class House : IBaseEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalNumber { get; set; }
        public string Comment { get; set; }

        public string Porch { get; set; }

        public int? SubnetId { get; set; }
        public virtual Subnet Subnet { get; set; }

        //public List<Client.Client> Clients { get; set; }
    }
}