using Api.House;

namespace Api.Network
{
    public class SubnetDto
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public int Mask { get; set; }
        public string Gateway { get; set; }

        //public HouseDto House { get; set; }
    }
}