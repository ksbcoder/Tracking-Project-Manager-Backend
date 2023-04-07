using Projects.Domain.Common;

namespace Projects.Domain.DTO.Task
{
    public class NewTaskDTO
    {
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Enums.Priority Priority { get; set; }
        public Enums.StateTask StateTask { get; set; }
    }
}