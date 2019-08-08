namespace Domain.Tariff
{
    public class JoinClientsTariffs
    {
        public int ClientId { get; set; }
        public virtual Client.Client Client { get; set; }

        public int TariffId { get; set; }
        public virtual Tariff Tariff { get; set; }
    }
}