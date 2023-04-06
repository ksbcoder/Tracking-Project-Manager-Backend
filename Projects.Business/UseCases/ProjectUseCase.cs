using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO;
using Projects.Domain.Entities;

namespace Projects.Business.UseCases
{
    public class ProjectUseCase : IProjectUseCase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectUseCase(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<NewProjectDTO> CreateProjectAsync(Project project)
        {
            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task<UpdateProjectDTO> DeleteProjectAsync(string idProject)
        {
            return await _projectRepository.DeleteProjectAsync(idProject);
        }

        public Task<Project> GetProjectByIdAsync(string idProject)
        {
            return _projectRepository.GetProjectByIdAsync(idProject);
        }

        public async Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project)
        {
            return await _projectRepository.UpdateProjectAsync(idProject, project);
        }
    }
}