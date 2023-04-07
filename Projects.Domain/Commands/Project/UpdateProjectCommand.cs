using Projects.Domain.Common;

namespace Projects.Domain.Commands.Project
{
    public class UpdateProjectCommand
    {
        public string LeaderID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DeadLine { get; set; }
        public Enums.StateProject StateProject { get; set; }
    }
}