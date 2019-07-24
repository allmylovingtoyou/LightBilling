using System.Collections.Generic;

namespace Domain.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalNumber { get; set; }
        public string Comment { get; set; }

        public List<Client.Client> Clients { get; set; }
    }
}