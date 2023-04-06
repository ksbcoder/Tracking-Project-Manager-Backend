using Users.Domain.Common;

namespace Users.Domain.Commands
{
    public class NewUserCommand
    {
        public string UidUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Enums.Roles Role { get; set; }
    }
}
