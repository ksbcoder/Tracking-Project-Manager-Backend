namespace Projects.Domain.Commands.Comment
{
    public class NewCommentCommand
    {
        public string UidUser { get; set; }
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
    }
}