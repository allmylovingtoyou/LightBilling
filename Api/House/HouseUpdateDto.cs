using Api.Network;

namespace Api.House
{
    /// <summary>
    /// Сущность дома
    /// </summary>
    public class HouseUpdateDto
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AdditionalNumber { get; set; }
        public string Comment { get; set; }

        public int SubnetId { get; set; }
    }
}