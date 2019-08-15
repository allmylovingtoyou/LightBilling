using System.Collections.Generic;
using Domain.Base;

namespace Domain.Tariff
{
    public class Tariff : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public TariffType Type { get; set; }

        public int InputRate { get; set; }
        
        public int OutputRate { get; set; }
        
        public double Cost { get; set; } = 0;
        
        public virtual ICollection<JoinClientsTariffs> JoinClients { get; set; }
    }
}