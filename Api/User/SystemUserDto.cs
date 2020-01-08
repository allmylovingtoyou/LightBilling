namespace Api.User
{
    public class SystemUserDto
    {
        public int? Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}