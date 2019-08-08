using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Network
{
    public class GreyAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public int? ClientId { get; set;}
        public virtual Client.Client Client { get; set;}
        
        public virtual WhiteAddress White { get; set; }
    }
}