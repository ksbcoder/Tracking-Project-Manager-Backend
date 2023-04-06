using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO;
using Projects.Domain.Entities;
using Projects.Infrastructure.Gateway;

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

        public async Task<NewProjectDTO> CreateProjectAsync(Project project)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();
            var projectToCreate = new Project(project);
            Project.SetDetailsProjectEntity(projectToCreate);

            Guard.Against.Null(projectToCreate, nameof(projectToCreate));
            Guard.Against.NullOrEmpty(projectToCreate.LeaderID, nameof(projectToCreate.LeaderID));
            Guard.Against.NullOrEmpty(projectToCreate.Name, nameof(projectToCreate.Name));
            Guard.Against.NullOrEmpty(projectToCreate.Description, nameof(projectToCreate.Description));
            Guard.Against.OutOfSQLDateRange(projectToCreate.CreatedAt, nameof(projectToCreate.CreatedAt));
            Guard.Against.OutOfRange(projectToCreate.EfficiencyRate, nameof(projectToCreate.EfficiencyRate), 0, 100);
            Guard.Against.EnumOutOfRange(projectToCreate.StateProject, nameof(projectToCreate.StateProject));


            string query = $"INSERT INTO {_tableNameProjects} (LeaderID, Name, Description, CreatedAt, OpenDate, DeadLine, " +
                            $"CompletedAt, EfficiencyRate, Phase, StateProject) " +
                            $"VALUES (@LeaderID, @Name, @Description, @CreatedAt, @OpenDate, @DeadLine, @CompletedAt, " +
                            $"@EfficiencyRate, @Phase, @StateProject)";

            var result = await connection.ExecuteAsync(query, projectToCreate);
            connection.Close();
            return _mapper.Map<NewProjectDTO>(projectToCreate);
        }

        public Task<UpdateProjectDTO> DeleteProjectAsync(string idProject)
        {
            throw new NotImplementedException();
        }

        public async Task<Project> GetProjectByIdAsync(string idProject)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject)
                                select p)
                                .SingleOrDefault();
            connection.Close();
            return projectFound == null ? _mapper.Map<Project>(Guard.Against.Null(projectFound, nameof(projectFound), 
                                           $"There is no a project with this ID: {idProject}"))
                                        : _mapper.Map<Project>(projectFound);
        }

        public async Task<UpdateProjectDTO> UpdateProjectAsync(string idProject, Project project)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == Guid.Parse(idProject)
                                select p)
                                .SingleOrDefault();

            var projectToUpdate = projectFound != null ? Project.SetNewAplicableValuesToProjectEntity(projectFound, project)
                                                        : Guard.Against.Null(projectFound, nameof(projectFound));

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