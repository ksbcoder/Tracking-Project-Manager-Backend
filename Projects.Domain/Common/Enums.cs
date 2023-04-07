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
            Assigned,
            Process,
            Completed
        }

        public enum Priority
        {
            High,
            Medium,
            Low
        }

        public enum StateInscription
        {
            Pending,
            Approved,
            Denied
        }

        public enum StateComment
        {
            Active,
            Deleted
        }
    }
}
