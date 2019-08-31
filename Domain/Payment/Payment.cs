using System;
using Domain.Base;

namespace Domain.Payment
{
    public class Payment : IBaseEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }

        public string Comment { get; set; }
        public PaymentType Type { get; set; }

        public virtual Client.Client Client { get; set; }
    }
}