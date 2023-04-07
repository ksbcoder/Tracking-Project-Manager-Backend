namespace Projects.Domain.Entities.Handlers
{
    public class TaskHandler
    {
        public static bool ValidateDeadLineDate(DateTime deadLineTask, Project project)
        {
            return deadLineTask.Date >= project.OpenDate?.Date
                    && deadLineTask.Date >= DateTime.Now.Date
                    && deadLineTask.Date <= project.DeadLine?.Date;
        }

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
    }
}
