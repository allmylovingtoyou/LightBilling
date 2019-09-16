using Domain.Base;

namespace Domain.Network
{
    public class WhiteAddress : IBaseEntity
    {
        public int Id { get; set; }
        public int? GrayAddressId { get; set; }
        public string Address { get; set; }

        public virtual GreyAddress GrayAddress { get; set; }
    }
}