using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.DTO;
using Projects.Domain.Entities;
using Projects.Domain.Entities.Handlers;
using Projects.Infrastructure.Gateway;
using System.Data;

namespace Projects.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProjects = "Projects";
        private readonly IMapper _mapper;

        public ProjectRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<UpdateProjectDTO> CompleteProjectAsync(string idProject)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject) && p.StateProject == Enums.StateProject.Active
                                && p.Phase == Enums.Phase.Started
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available or was stop already. ID: {idProject}.");

            projectFound.SetPhase(Enums.Phase.Completed);
            projectFound.SetStateProject(Enums.StateProject.Inactive);
            projectFound.SetCompletedAt(DateTime.Now);
            int expectedDays = ProjectHandler.CalculateDaysFromTo(projectFound.OpenDate, projectFound.DeadLine);
            int realDays = ProjectHandler.CalculateDaysFromTo(projectFound.OpenDate, projectFound.CompletedAt);
            projectFound.SetEfficiencyRate(ProjectHandler.CalculateEfficiencyRate(expectedDays, realDays));

            string query = $"UPDATE {_tableNameProjects} SET Phase = @Phase, StateProject = @StateProject, " +
                            $"CompletedAt = @CompletedAt " +
                            $"WHERE ProjectID = @ProjectID";
            var result = await connection.ExecuteAsync(query, projectFound);
            connection.Close();

            return result == 0 ? _mapper.Map<UpdateProjectDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateProjectDTO>(projectFound);
        }

        public async Task<NewProjectDTO> CreateProjectAsync(Project project)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            Project.SetDetailsProjectEntity(project);

            Guard.Against.Null(project, nameof(project));
            Guard.Against.NullOrEmpty(project.LeaderID, nameof(project.LeaderID));
            Guard.Against.NullOrEmpty(project.Name, nameof(project.Name));
            Guard.Against.NullOrEmpty(project.Description, nameof(project.Description));
            Guard.Against.OutOfSQLDateRange(project.CreatedAt, nameof(project.CreatedAt));
            Guard.Against.OutOfRange(project.EfficiencyRate, nameof(project.EfficiencyRate), 0, 100);
            Guard.Against.EnumOutOfRange(project.StateProject, nameof(project.StateProject));


            string query = $"INSERT INTO {_tableNameProjects} (LeaderID, Name, Description, CreatedAt, OpenDate, DeadLine, " +
                            $"CompletedAt, EfficiencyRate, Phase, StateProject) " +
                            $"VALUES (@LeaderID, @Name, @Description, @CreatedAt, @OpenDate, @DeadLine, @CompletedAt, " +
                            $"@EfficiencyRate, @Phase, @StateProject)";

            var result = await connection.ExecuteAsync(query, project);
            connection.Close();
            return result == 0 ? _mapper.Map<NewProjectDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<NewProjectDTO>(project);
        }

        public async Task<UpdateProjectDTO> DeleteProjectAsync(string idProject)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject) && p.StateProject != Enums.StateProject.Deleted
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound), $"There is no a project available with this ID: {idProject}.");

            projectFound.SetStateProject(Enums.StateProject.Deleted);
            string query = $"UPDATE {_tableNameProjects} SET StateProject = @StateProject " +
                            $"WHERE ProjectID = @ProjectID";
            var result = await connection.ExecuteAsync(query, projectFound);
            connection.Close();

            return result == 0 ? _mapper.Map<UpdateProjectDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateProjectDTO>(projectFound);
        }

        public async Task<List<Project>> GetActiveProjectsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectsFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                 where p.StateProject == Enums.StateProject.Active
                                 select p)
                                 .ToList();
            connection.Close();
            return projectsFound == null ? _mapper.Map<List<Project>>(Guard.Against.Null(projectsFound, nameof(projectsFound),
                                            $"There is no a project available with this ID: {projectsFound}."))
                                         : _mapper.Map<List<Project>>(projectsFound);
        }

        public async Task<List<Project>> GetAllNoDeletedProjectsAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectsFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                 where p.StateProject != Enums.StateProject.Deleted
                                 select p)
                                 .ToList();
            connection.Close();
            return projectsFound == null ? _mapper.Map<List<Project>>(Guard.Against.Null(projectsFound, nameof(projectsFound),
                                            $"There is no a project available with this ID: {projectsFound}."))
                                         : _mapper.Map<List<Project>>(projectsFound);
        }

        public async Task<Project> GetProjectByIdAsync(string idProject)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject) && p.StateProject != Enums.StateProject.Deleted
                                select p)
                                .SingleOrDefault();
            connection.Close();
            return projectFound == null ? _mapper.Map<Project>(Guard.Against.Null(projectFound, nameof(projectFound),
                                           $"There is no a project available with this ID: {idProject}."))
                                        : _mapper.Map<Project>(projectFound);
        }

        public async Task<UpdateProjectDTO> OpenProjectAsync(string idProject, Project project)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject) 
                                        && p.StateProject == Enums.StateProject.Active
                                        && p.OpenDate == null && p.DeadLine == null && p.Phase == null
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound), 
                $"There is no a project available or was open already. ID: {idProject}.");

            ProjectHandler.SetDetailsWhenOpenProject(projectFound, project);
            string query = $"UPDATE {_tableNameProjects} SET OpenDate = @OpenDate, DeadLine = @DeadLine, Phase = @Phase " +
                            $"WHERE ProjectID = @ProjectID";
            var result = await connection.ExecuteAsync(query, projectFound);
            connection.Close() ;

            return result == 0 ? _mapper.Map<UpdateProjectDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateProjectDTO>(projectFound);
        }

        public async Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject) && p.StateProject != Enums.StateProject.Deleted
                                select p)
                                .SingleOrDefault();

            var projectToUpdate = projectFound != null ? ProjectHandler.SetNewAplicableValuesToProjectEntity(projectFound, project)
                                                        : Guard.Against.Null(projectFound, nameof(projectFound),
                                                            $"There is no a project available with this ID: {idProject}.");

            Guard.Against.Null(projectToUpdate, nameof(projectToUpdate));
            Guard.Against.NullOrEmpty(projectToUpdate.ProjectID, nameof(projectToUpdate.ProjectID));
            Guard.Against.NullOrEmpty(projectToUpdate.LeaderID, nameof(projectToUpdate.LeaderID));
            Guard.Against.NullOrEmpty(projectToUpdate.Name, nameof(projectToUpdate.Name));
            Guard.Against.NullOrEmpty(projectToUpdate.Description, nameof(projectToUpdate.Description));
            Guard.Against.OutOfSQLDateRange(projectToUpdate.CreatedAt, nameof(projectToUpdate.CreatedAt));
            Guard.Against.OutOfRange(projectToUpdate.EfficiencyRate, nameof(projectToUpdate.EfficiencyRate), 0, 100);
            Guard.Against.EnumOutOfRange(projectToUpdate.StateProject, nameof(projectToUpdate.StateProject));

            string query = $"UPDATE {_tableNameProjects} SET " +
                            $"LeaderID = @LeaderID, Name = @Name, Description = @Description, CreatedAt = @CreatedAt, " +
                            $"OpenDate = @OpenDate, DeadLine = @DeadLine, CompletedAt = @CompletedAt, " +
                            $"EfficiencyRate = @EfficiencyRate, Phase = @Phase, StateProject = @StateProject " +
                            $"WHERE ProjectID = @ProjectID";

            var result = await connection.ExecuteAsync(query, projectToUpdate);
            connection.Close();
            return result == 0 ? _mapper.Map<UpdateProjectDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateProjectDTO>(projectToUpdate);
        }
    }
}