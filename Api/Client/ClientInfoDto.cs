namespace Api.Client
{
    public class ClientInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }

        public double Balance { get; set; }

        public string Status { get; set; }

        public bool IsActive { get; set; }
    }
}