using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projects.Business.Gateway;
using Projects.Domain.Commands;
using Projects.Domain.DTO;
using Projects.Domain.Entities;

namespace Projects.API.Controllers
{
    [Route("api/[controller]")]
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
    }
}