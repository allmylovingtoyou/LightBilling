namespace Api.Client
{
    public class ClientFilter
    {
        public int? Id { get; set; }
        public string Login { get; set; }
       
        public string HwIpAddress { get; set; }
        public string HwPort { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int HouseId { get; set; }
        public string Composite { get; set; }
        public string FullName { get; set; }
    }
}