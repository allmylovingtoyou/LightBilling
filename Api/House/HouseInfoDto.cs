namespace Api.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class HouseInfoDto
    {
        public int? Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalNumber { get; set; }
        public string Comment { get; set; }
        public string Porch { get; set; }
        public string SubnetString { get; set; }
    }
}