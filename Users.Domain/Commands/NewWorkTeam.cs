using Users.Domain.Common;

namespace Users.Domain.Commands
{
    public class NewWorkTeam
    {
        public decimal EfficiencyRate { get; set; }
        public Enums.StateTeam StateTeam { get; set; }
    }
}
