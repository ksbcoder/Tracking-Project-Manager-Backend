using Projects.Domain.Common;

namespace Projects.Domain.DTO.Comment
{
    public class UpdateCommentDTO
    {
        public int CommentID { get; set; }
        public string UidUser { get; set; }
        public Guid ProjectID { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public Enums.StateComment StateComment { get; set; }
    }
}