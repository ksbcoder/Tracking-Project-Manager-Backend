using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Task
    {
        public int TaskID { get; private set; }
        public Guid ProjectID { get; private set; }
        public string Description { get; private set; }
        public string CreatedBy { get; private set; }
        public string? AssignedTo { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? AssignedAt { get; private set; }
        public DateTime DeadLine { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public Enums.Priority Priority { get; private set; }
        public Enums.StateTask StateTask { get; private set; }

        #region constructors
        public Task() { }
        #endregion 

        #region methods
        public static Task SetDetailsTaskEntity(Task task)
        {
            task.CreatedAt = DateTime.Now;
            task.StateTask = Enums.StateTask.Active;
            return task;
        }
        #endregion

        #region setters
        public void SetTaskID(int taskID)
        {
            TaskID = taskID;
        }
        public void SetProjectID(Guid projectID)
        {
            ProjectID = projectID;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetCreatedBy(string createdBy)
        {
            CreatedBy = createdBy;
        }
        public void SetAssignedTo(string? assignedTo)
        {
            AssignedTo = assignedTo;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetAssignedAt(DateTime? assignedAt)
        {
            AssignedAt = assignedAt;
        }
        public void SetDeadLine(DateTime deadline)
        {
            DeadLine = deadline;
        }
        public void SetCompletedAt(DateTime? completedAt)
        {
            CompletedAt = completedAt;
        }
        public void SetPriority(Enums.Priority priority)
        {
            Priority = priority;
        }
        public void SetStateTask(Enums.StateTask stateTask)
        {
            StateTask = stateTask;
        }
        #endregion
    }
}