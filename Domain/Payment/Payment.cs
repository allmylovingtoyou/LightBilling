using System;
using Domain.Base;

namespace Domain.Payment
{
    public class Payment : IBaseEntity
    {
        public int Id { get; set; }
        public int ClientId { get; set;}
        public DateTime DateTime { get; set; }
        public double Amount { get; set; }
    }
}