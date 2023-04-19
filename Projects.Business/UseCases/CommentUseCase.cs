using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO.Comment;
using Projects.Domain.Entities;

namespace Projects.Business.UseCases
{
    public class CommentUseCase : ICommentUseCase
    {
        private readonly ICommentRepository _commentRepository;
        
        public CommentUseCase(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<NewCommentDTO> CreateCommentAsync(Comment comment)
        {
            return await _commentRepository.CreateCommentAsync(comment);
        }

        public async Task<UpdateCommentDTO> DeleteCommentAsync(int idComment, string idUser)
        {
            return await _commentRepository.DeleteCommentAsync(idComment, idUser);
        }

        public async Task<List<Comment>> GetAllCommentsByProjectIdAsync(string idProject)
        {
            return await _commentRepository.GetAllCommentsByProjectIdAsync(idProject);
        }
    }
}