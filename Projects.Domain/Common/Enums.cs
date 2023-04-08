namespace Projects.Domain.Common
{
    public class Enums
    {
        public enum StateProject
        {
            Active,
            Inactive,
            Deleted
        }

        public enum Phase
        {
            Started,
            Completed
        }

        public enum StateTask
        {
            Active,
            Assigned,
            Completed,
            Deleted
        }

        public enum Priority
        {
            Low,
            Medium,
            High
        }

        public enum StateInscription
        {
            Pending,
            Approved,
            Denied,
            Deleted
        }

        public enum StateComment
        {
            Active,
            Deleted
        }
    }
}
