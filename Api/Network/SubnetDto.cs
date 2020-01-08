using System.Collections.Generic;

namespace Api.Network
{
    public class SubnetDto
    {
        public int? Id { get; set; }
        public string Net { get; set; }
        public int Mask { get; set; }
        public string Gateway { get; set; }
        
        public List<GreyAddressDto> Addresses { get; set;}

        //public HouseDto House { get; set; }
    }
}