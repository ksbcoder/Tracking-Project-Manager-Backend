﻿using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO.Project;
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

        public async Task<UpdateProjectDTO> CompleteProjectAsync(string idProject)
        {
            return await _projectRepository.CompleteProjectAsync(idProject);
        }

        public async Task<NewProjectDTO> CreateProjectAsync(Project project)
        {
            return await _projectRepository.CreateProjectAsync(project);
        }

        public async Task<UpdateProjectDTO> DeleteProjectAsync(string idProject)
        {
            return await _projectRepository.DeleteProjectAsync(idProject);
        }

        public async Task<List<Project>> GetActiveProjectsAsync()
        {
            return await _projectRepository.GetActiveProjectsAsync();
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllProjectsAsync();

        }
        public async Task<Project> GetProjectByIdAsync(string idProject)
        {
            return await _projectRepository.GetProjectByIdAsync(idProject);
        }

        public async Task<List<Project>> GetProjectsActiveByLeaderIdAsync(string leaderId)
        {
            return await _projectRepository.GetProjectsActiveByLeaderIdAsync(leaderId);
        }

        public async Task<List<Project>> GetProjectsByLeaderIdAsync(string leaderId)
        {
            return await _projectRepository.GetProjectsByLeaderIdAsync(leaderId);
        }

        public async Task<UpdateProjectDTO> OpenProjectAsync(string idProject, string uidUser, Project project)
        {
            return await _projectRepository.OpenProjectAsync(idProject, uidUser, project);
        }

        public async Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project)
        {
            return await _projectRepository.UpdateProjectAsync(idProject, project);
        }
    }
}