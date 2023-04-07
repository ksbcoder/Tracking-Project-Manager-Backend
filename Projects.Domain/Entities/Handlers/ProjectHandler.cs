using Projects.Domain.Common;

namespace Projects.Domain.Entities.Handlers
{
    public class ProjectHandler : Project
    {
        public static Project SetNewAplicableValuesToProjectEntity(Project oldProject, Project newProject)
        {
            oldProject.SetProjectID(oldProject.ProjectID);
            oldProject.SetCreatedAt(oldProject.CreatedAt);
            oldProject.SetOpenDate(oldProject.OpenDate);
            oldProject.SetCompletedAt(oldProject.CompletedAt);
            oldProject.SetEfficiencyRate(oldProject.EfficiencyRate);
            oldProject.SetPhase(oldProject.Phase);

            oldProject.SetLeaderID(newProject.LeaderID);
            oldProject.SetName(newProject.Name);
            oldProject.SetDescription(newProject.Description);
            oldProject.SetDeadLine(newProject.DeadLine);
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

        public static int CalculateDaysFromTo(DateTime? dateFrom, DateTime? dateTo)
        {
            TimeSpan dateDifference = (TimeSpan)(dateTo - dateFrom);
            int days = dateDifference.Days <= 0 ? 1 : dateDifference.Days;
            return days;
        }

        public static decimal CalculateEfficiencyRate(int expectedDays, int realDays)
        {
            decimal efficiencyRate = ((decimal)expectedDays / realDays) * 100;
            efficiencyRate = Math.Round(efficiencyRate, 2);
            efficiencyRate = efficiencyRate < 0 ? 0 : efficiencyRate;
            efficiencyRate = efficiencyRate > 100 ? 100 : efficiencyRate;
            return efficiencyRate;
        }
    }
}