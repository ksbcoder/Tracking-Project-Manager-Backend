using Projects.Domain.Common;

namespace Projects.Domain.DTO
{
    public class UpdateProjectDTO
    {
        public Guid ProjectID { get; set; }
        public string LeaderID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public DateTime? CompletedAt { get; set; }
        public decimal EfficiencyRate { get; set; }
        public Enums.Phase? Phase { get; set; }
        public Enums.StateProject StateProject { get; set; }
    }
}