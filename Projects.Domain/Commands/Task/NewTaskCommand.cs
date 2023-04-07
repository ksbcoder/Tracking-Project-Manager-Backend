using Projects.Domain.Common;

namespace Projects.Domain.Commands.Task
{
    public class NewTaskCommand
    {
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Deadline { get; set; }
        public Enums.Priority Priority { get; set; }
    }
}