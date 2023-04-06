using Users.Domain.Common;

namespace Users.Domain.DTO
{
    public class UpdateUserDTO
    {
        public string UserID { get; set; }
        public string UidUser { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public decimal EfficiencyRate { get; set; }
        public int TasksCompleted { get; set; }
        public Enums.Roles Role { get; set; }
        public Enums.StateUser StateUser { get; set; }
    }
}
