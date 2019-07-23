using System.Collections.Generic;

namespace Domain.House
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public List<Client.Client> Clients { get; set; }
    }
}