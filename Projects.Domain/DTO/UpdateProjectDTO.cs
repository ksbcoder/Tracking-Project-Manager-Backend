using Projects.Domain.Common;

namespace Projects.Domain.DTO
{
    public class UpdateProjectDTO
    {
        public Guid ProjectID { get; private set; }
        public string LeaderID { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? OpenDate { get; private set; }
        public DateTime? DeadLine { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public decimal EfficiencyRate { get; private set; }
        public Enums.Phase? Phase { get; private set; }
        public Enums.StateProject StateProject { get; private set; }
    }
}
