namespace Domain.Network
{
    public class WhiteAddress
    {
        public int Id { get; set; }
        public int GrayAddressId { get; set; }
        public string Address { get; set; }

        public virtual GreyAddress GrayAddress { get; set; }
    }
}