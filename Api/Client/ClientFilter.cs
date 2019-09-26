namespace Api.Client
{
    public class ClientFilter
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string HwIpAddress { get; set; }
        public string HwPort { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public int HouseId { get; set; }
    }
}