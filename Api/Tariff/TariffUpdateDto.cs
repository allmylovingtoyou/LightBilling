namespace Api.Tariff
{
    public class TariffUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public TariffTypeDto Type { get; set; }

        public int InputRate { get; set; }
        
        public int OutputRate { get; set; }
    }
}