using Domain.Base;

namespace Domain.User
{
    public class SystemUser : IBaseEntity
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isActive { get; set; }
        public string Role { get; set; }
    }
}