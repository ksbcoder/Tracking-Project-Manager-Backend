using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Task
    {
        public int TaskID { get; set; }
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string? AssignedTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? AssignedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? CompletedAt { get; set; }
        public Enums.Priority Priority { get; set; }
        public Enums.StateTask StateTask { get; set; }

        public Task() { }

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
        public void SetAssignedTo(string assignedTo)
        {
            AssignedTo = assignedTo;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetAssignedAt(DateTime assignedAt)
        {
            AssignedAt = assignedAt;
        }
        public void SetDeadline(DateTime deadline)
        {
            Deadline = deadline;
        }
        public void SetCompletedAt(DateTime completedAt)
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
