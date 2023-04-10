namespace Projects.Domain.Entities.Handlers
{
    public class TaskHandler
    {
        public static Task SetNewAplicableValuesToTaskEntity(Task oldTask, Task newTask)
        {
            oldTask.SetCreatedBy(oldTask.CreatedBy);
            oldTask.SetCreatedAt(oldTask.CreatedAt);
            oldTask.SetCompletedAt(oldTask.CompletedAt);
            oldTask.SetStateTask(oldTask.StateTask);

            oldTask.SetProjectID(newTask.ProjectID);
            oldTask.SetDescription(newTask.Description);
            oldTask.SetAssignedTo(newTask.AssignedTo);
            oldTask.SetAssignedAt(newTask.AssignedAt);
            oldTask.SetDeadLine(newTask.DeadLine);
            oldTask.SetPriority(newTask.Priority);
            return oldTask;
        }
        public static decimal CalculateEfficiencyRateUser(int expectedDays, int realDays)
        {
            decimal efficiencyRate = ((decimal)expectedDays / realDays) * 100;
            efficiencyRate = Math.Round(efficiencyRate, 2);
            efficiencyRate = efficiencyRate < 0 ? 0 : efficiencyRate;
            efficiencyRate = efficiencyRate > 100 ? 100 : efficiencyRate;
            return efficiencyRate;
        }
    }
}