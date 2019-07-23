namespace Domain.Client
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public int HouseId { get; set; }
        public House.House House { get; set; }
    }
}