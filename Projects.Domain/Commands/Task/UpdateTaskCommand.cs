using Projects.Domain.Common;

namespace Projects.Domain.Commands.Task
{
    public class UpdateTaskCommand
    {
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime Deadline { get; set; }
        public Enums.Priority Priority { get; set; }
    }
}