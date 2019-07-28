namespace Domain.Network
{
    public class FullAddress
    {
        public int Id { get; set; }
        public string Grey { get; set; }
        public virtual WhiteAddress White { get; set; }
        public virtual Subnet Subnet { get; set; }
    }
}