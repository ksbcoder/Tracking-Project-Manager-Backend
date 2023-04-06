using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Project
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

        #region constructors
        public Project() { }

        public Project(Project project)
        {
            ProjectID = Guid.NewGuid();
            LeaderID = project.LeaderID;
            Name = project.Name;
            Description = project.Description;
            CreatedAt = project.CreatedAt;
            OpenDate = project.OpenDate;
            DeadLine = project.DeadLine;
            CompletedAt = project.CompletedAt;
            EfficiencyRate = project.EfficiencyRate;
            Phase = project.Phase;
            StateProject = project.StateProject;
        }
        #endregion

        #region methods
        public static Project SetDetailsProjectEntity(Project project)
        {
            project.CreatedAt = DateTime.Now;
            project.EfficiencyRate = 0;
            project.StateProject = Enums.StateProject.Active;
            return project;
        }

        public static Project SetNewAplicableValuesToProjectEntity(Project oldProject, Project newProject)
        {
            oldProject.ProjectID = oldProject.ProjectID;
            oldProject.CreatedAt = oldProject.CreatedAt;

            oldProject.LeaderID = newProject.LeaderID;
            oldProject.Name = newProject.Name;
            oldProject.Description = newProject.Description;
            oldProject.OpenDate = newProject.OpenDate;
            oldProject.DeadLine = newProject.DeadLine;
            oldProject.CompletedAt = newProject.CompletedAt;
            oldProject.EfficiencyRate = newProject.EfficiencyRate;
            oldProject.Phase = newProject.Phase;
            oldProject.StateProject = newProject.StateProject;
            return oldProject;
        }
        #endregion

        #region setters
        public void SetProjecID(Guid projecID)
        {
            ProjectID = projecID;
        }
        public void SetLeaderID(string leaderID)
        {
            LeaderID = leaderID;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetOpenDate(DateTime openDate)
        {
            OpenDate = openDate;
        }
        public void SetDeadLine(DateTime deadLine)
        {
            DeadLine = deadLine;
        }
        public void SetCompletedAt(DateTime completedAt)
        {
            CompletedAt = completedAt;
        }
        public void SetEfficiencyRate(decimal efficiencyRate)
        {
            EfficiencyRate = efficiencyRate;
        }
        public void SetPhase(Enums.Phase phase)
        {
            Phase = phase;
        }
        public void SetStateProject(Enums.StateProject stateProject)
        {
            StateProject = stateProject;
        }
        #endregion
    }
}