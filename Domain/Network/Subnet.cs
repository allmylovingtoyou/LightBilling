using System.Collections.Generic;
using Domain.Base;

namespace Domain.Network
{
    public class Subnet : IBaseEntity
    {
        public int Id { get; set; }
        public string Net { get; set; }
        public int Mask { get; set; }
        public string Gateway { get; set;}
        
        public virtual ICollection<GreyAddress> Addresses { get; set;}
        public virtual ICollection<House.House> Houses { get; set;}
    }
}