using Projects.Domain.DTO.Comment;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway
{
    public interface ICommentUseCase
    {
        Task<NewCommentDTO> CreateCommentAsync(Comment comment);
        Task<UpdateCommentDTO> DeleteCommentAsync(int idComment, string idUser);
        Task<List<Comment>> GetAllCommentsByProjectIdAsync(string idProject);
    }
}