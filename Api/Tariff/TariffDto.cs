namespace Api.Tariff
{
    public class TariffDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Comment { get; set; }

        public bool IsPeriodic { get; set; }

        public TariffTypeDto Type { get; set; }

        public string TypeString => Type.ToString();

        public int InputRate { get; set; }
        
        public int OutputRate { get; set; }
    }
}