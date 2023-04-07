namespace Projects.Domain.Commands.Project
{
    public class NewProjectCommand
    {
        public string LeaderID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
