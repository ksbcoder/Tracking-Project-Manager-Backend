using Projects.Domain.Common;

namespace Projects.Domain.Entities.Handlers
{
    public class ProjectHandler : Project
    {
        public static Project SetNewAplicableValuesToProjectEntity(Project oldProject, Project newProject)
        {
            oldProject.SetProjecID(oldProject.ProjectID);
            oldProject.SetCreatedAt(oldProject.CreatedAt);

            oldProject.SetLeaderID(newProject.LeaderID);
            oldProject.SetName(newProject.Name);
            oldProject.SetDescription(newProject.Description);
            oldProject.SetOpenDate(newProject.OpenDate);
            oldProject.SetDeadLine(newProject.DeadLine);
            oldProject.SetCompletedAt(newProject.CompletedAt);
            oldProject.SetEfficiencyRate(newProject.EfficiencyRate);
            oldProject.SetPhase(newProject.Phase);
            oldProject.SetStateProject(newProject.StateProject);
            return oldProject;
        }

        public static Project SetDetailsWhenOpenProject(Project oldProject, Project newProject)
        {
            oldProject.SetOpenDate(DateTime.Now);
            oldProject.SetDeadLine(newProject.DeadLine);
            oldProject.SetPhase(Enums.Phase.Started);
            return oldProject;
        }
    }
}
