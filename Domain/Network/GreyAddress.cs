using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.Network
{
    public class GreyAddress : IBaseEntity
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public int? ClientId { get; set;}
        public virtual Client.Client Client { get; set;}
        
        public virtual WhiteAddress White { get; set; }
    }
}