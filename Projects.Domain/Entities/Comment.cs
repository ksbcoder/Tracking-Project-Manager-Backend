using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Comment
    {
        public int CommentID { get; private set; }
        public string UidUser { get; private set; }
        public Guid ProjectID { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Enums.StateComment StateComment { get; private set; }

        #region constructors
        public Comment() { }
        #endregion

        #region methods
        public static Comment SetDetailsCommentEntity(Comment comment)
        {
            comment.CreatedAt = DateTime.Now;
            comment.StateComment = Enums.StateComment.Active;
            return comment;
        }
        #endregion

        #region setters
        public void SetCommentID(int commentID)
        {
            CommentID = commentID;
        }
        public void SetProjectID(Guid projectID)
        {
            ProjectID = projectID;
        }
        public void SetUserID(string userID)
        {
            UidUser = userID;
        }
        public void SetDescription(string description)
        {
            Description = description;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetStateComment(Enums.StateComment stateComment)
        {
            StateComment = stateComment;
        }
        #endregion
    }
}