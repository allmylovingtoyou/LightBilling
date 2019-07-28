using System.Collections.Generic;

namespace Domain.Network
{
    public class Subnet
    {
        public int Id { get; set; }
        public string Address { get; set;}
        public int Mask { get; set; }
        public string Gateway { get; set;}

        public virtual ICollection<House.House> Houses { get; set;}
    }
}