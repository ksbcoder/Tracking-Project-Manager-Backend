using Users.Domain.Common;

namespace Users.Domain.Commands
{
    public class NewUser
    {
        public string UidUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Enums.Roles Role { get; set; }
        public Enums.StateUser StateUser { get; set; }
    }
}
