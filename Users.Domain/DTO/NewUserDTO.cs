using Users.Domain.Common;

namespace Users.Domain.DTO
{
    public class NewUserDTO
    {
        public string UidUser { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public decimal EfficiencyRate { get; private set; }
        public int TasksCompleted { get; private set; }
        public Enums.Roles Role { get; private set; }
        public Enums.StateUser StateUser { get; private set; }
    }
}
