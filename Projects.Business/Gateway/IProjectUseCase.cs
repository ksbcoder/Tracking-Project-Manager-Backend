using Projects.Domain.DTO.Project;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway
{
    public interface IProjectUseCase
    {
        Task<NewProjectDTO> CreateProjectAsync(Project project);
        Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project);
        Task<UpdateProjectDTO> DeleteProjectAsync(string idProject);
        Task<Project> GetProjectByIdAsync(string idProject);
        //use cases
        Task<List<Project>> GetProjectsByLeaderIdAsync(string leaderId);
        Task<List<Project>> GetProjectsActiveByLeaderIdAsync(string leaderId);
        Task<List<Project>> GetActiveProjectsAsync();
        Task<List<Project>> GetAllProjectsAsync();
        Task<UpdateProjectDTO> OpenProjectAsync(string idProject, string uidUser, Project project);
        Task<UpdateProjectDTO> CompleteProjectAsync(string idProject);
    }
}