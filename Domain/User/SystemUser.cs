namespace Domain.User
{
    public class SystemUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public string Role { get; set; }
    }
}