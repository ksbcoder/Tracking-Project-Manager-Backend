using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projects.Business.Gateway;
using Projects.Domain.Commands.Comment;
using Projects.Domain.DTO.Comment;
using Projects.Domain.Entities;

namespace Projects.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentUseCase _commentUseCase;
        private readonly IMapper _mapper;

        public CommentController(ICommentUseCase commentUseCase, IMapper mapper)
        {
            _commentUseCase = commentUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewCommentDTO> CreateCommentAsync([FromBody] NewCommentCommand newCommentCommand)
        {
            return await _commentUseCase.CreateCommentAsync(_mapper.Map<Comment>(newCommentCommand));
        }

        [HttpDelete("ID")]
        public async Task<UpdateCommentDTO> DeleteCommentAsync(int idComment, string idUser)
        {
            return await _commentUseCase.DeleteCommentAsync(idComment, idUser);
        }

        [HttpGet("All/ProjectID")]
        public async Task<List<Comment>> GetAllCommentsByProjectIdAsync(string idProject)
        {
            return await _commentUseCase.GetAllCommentsByProjectIdAsync(idProject);
        }
    }
}