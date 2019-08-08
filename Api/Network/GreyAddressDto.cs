namespace Api.Network
{
    public class GreyAddressDto
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public WhiteAddressDto White { get; set; }
    }
}