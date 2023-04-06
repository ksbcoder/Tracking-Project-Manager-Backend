using Projects.Domain.DTO;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway
{
    public interface IProjectUseCase
    {
        Task<NewProjectDTO> CreateProjectAsync(Project project);
        Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project);
        Task<UpdateProjectDTO> DeleteProjectAsync(string idProject);
        Task<Project> GetProjectByIdAsync(string idProject);
    }
}