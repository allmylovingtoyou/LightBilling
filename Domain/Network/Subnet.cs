using System.Collections.Generic;

namespace Domain.Network
{
    public class Subnet
    {
        public int Id { get; set; }
        public int Mask { get; set; }
        public string Gateway { get; set;}
        
        public virtual ICollection<GreyAddress> Addresses { get; set;}
        public virtual ICollection<House.House> Houses { get; set;}
    }
}