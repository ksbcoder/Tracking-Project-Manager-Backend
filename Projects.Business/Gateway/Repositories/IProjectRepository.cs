using Projects.Domain.DTO.Project;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway.Repositories
{
    public interface IProjectRepository
    {
        Task<NewProjectDTO> CreateProjectAsync(Project project);
        Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project);
        Task<UpdateProjectDTO> DeleteProjectAsync(string idProject);
        Task<Project> GetProjectByIdAsync(string idProject);
        //use cases
        Task<List<Project>> GetActiveProjectsAsync();
        Task<List<Project>> GetAllNoDeletedProjectsAsync();
        Task<UpdateProjectDTO> OpenProjectAsync(string idProject, Project project);
        Task<UpdateProjectDTO> CompleteProjectAsync(string idProject);
    }
}