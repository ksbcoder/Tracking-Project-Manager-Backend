using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projects.Business.Gateway;
using Projects.Domain.Commands.Project;
using Projects.Domain.DTO.Project;
using Projects.Domain.Entities;

namespace Projects.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectUseCase _projectUseCase;
        private readonly IMapper _mapper;

        public ProjectController(IProjectUseCase projectUseCase, IMapper mapper)
        {
            _projectUseCase = projectUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewProjectDTO> CreateProjectAsync([FromBody] NewProjectCommand newProjectCommand)
        {
            return await _projectUseCase.CreateProjectAsync(_mapper.Map<Project>(newProjectCommand));
        }

        [HttpPut("ID")]
        public async Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, [FromBody] UpdateProjectCommand updateProjectCommand)
        {
            return await _projectUseCase.UpdateProjectAsync(idProject, _mapper.Map<Project>(updateProjectCommand));
        }

        [HttpPut("OpenProject/ID")]
        public async Task<UpdateProjectDTO> OpenProjectAsync(string idProject, string uidUser, [FromBody] OpenProjectCommand openProjectCommand)
        {
            return await _projectUseCase.OpenProjectAsync(idProject, uidUser, _mapper.Map<Project>(openProjectCommand));
        }

        [HttpPut("CompleteProject/ID")]
        public async Task<UpdateProjectDTO> CompleteProjectAsync(string idProject)
        {
            return await _projectUseCase.CompleteProjectAsync(idProject);
        }

        [HttpDelete("ID")]
        public async Task<UpdateProjectDTO> DeleteProjectAsync(string idProject)
        {
            return await _projectUseCase.DeleteProjectAsync(idProject);
        }

        [HttpGet("ID")]
        public async Task<Project> GetProjectByIdAsync(string idProject)
        {
            return await _projectUseCase.GetProjectByIdAsync(idProject);
        }

        [HttpGet("LeaderID")]
        public async Task<List<Project>> GetProjectsByLeaderIdAsync(string leaderId)
        {
            return await _projectUseCase.GetProjectsByLeaderIdAsync(leaderId);
        }

        [HttpGet("Active/LeaderID")]
        public async Task<List<Project>> GetProjectsActiveByLeaderIdAsync(string leaderId)
        {
            return await _projectUseCase.GetProjectsActiveByLeaderIdAsync(leaderId);
        }

        [HttpGet("ActiveOnly")]
        public async Task<List<Project>> GetActiveProjectsAsync()
        {
            return await _projectUseCase.GetActiveProjectsAsync();
        }

        [HttpGet("AllNoDeleted")]
        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectUseCase.GetAllProjectsAsync();
        }
    }
}