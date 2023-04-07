using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.DTO.Task;
using Projects.Domain.Entities;
using Projects.Domain.Entities.Handlers;
using Projects.Infrastructure.Gateway;

namespace Projects.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProjects = "Projects";
        private readonly string _tableNameTasks = "Tasks";
        private readonly IMapper _mapper;

        public TaskRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }
        public Task<UpdateTaskDTO> AssignTaskAsync(int idTask, string uidUser)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateTaskDTO> CompleteTaskAsync(int idTask)
        {
            throw new NotImplementedException();
        }

        public async Task<NewTaskDTO> CreateTaskAsync(Domain.Entities.Task task)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == task.ProjectID && p.StateProject == Enums.StateProject.Active
                                && p.Phase == Enums.Phase.Started
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available with this ID: {task.ProjectID}.");

            var viableDeadlineDate = TaskHandler.ValidateDeadLineDate(task.DeadLine, projectFound);
            if (!viableDeadlineDate)
            {
                Guard.Against.Default(viableDeadlineDate, nameof(viableDeadlineDate),
                    $"Task deadline: {task.DeadLine:dd/MM/yyyy} " +
                    $"is greater than project deadline: {projectFound.DeadLine:dd/MM/yyyy} " +
                    $"or less than project open date: {projectFound.OpenDate:dd/MM/yyyy}.");
            }
            Domain.Entities.Task.SetDetailsTaskEntity(task);

            Guard.Against.Null(task, nameof(task));
            Guard.Against.NullOrEmpty(task.ProjectID, nameof(task.ProjectID));
            Guard.Against.NullOrEmpty(task.Description, nameof(task.Description));
            Guard.Against.NullOrEmpty(task.CreatedBy, nameof(task.CreatedBy));
            Guard.Against.OutOfSQLDateRange(task.CreatedAt, nameof(task.CreatedAt));
            Guard.Against.OutOfSQLDateRange(task.DeadLine, nameof(task.DeadLine));
            Guard.Against.EnumOutOfRange(task.Priority, nameof(task.Priority));
            Guard.Against.EnumOutOfRange(task.StateTask, nameof(task.StateTask));

            string query = $"INSERT INTO {_tableNameTasks} (ProjectID, Description, CreatedBy, AssignedTo, CreatedAt, " +
                            $"AssignedAt, Deadline, CompletedAt, Priority, StateTask) " +
                            $"VALUES (@ProjectID, @Description, @CreatedBy, @AssignedTo, @CreatedAt, @AssignedAt, " +
                            $"@DeadLine, @CompletedAt, @Priority, @StateTask)";

            var result = await connection.ExecuteAsync(query, task);
            connection.Close();
            return result == 0 ? _mapper.Map<NewTaskDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<NewTaskDTO>(task);
        }

        public async Task<UpdateTaskDTO> DeleteTaskAsync(int idTask)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var taskFound = await GetTaskByIdAsync(idTask);

            Guard.Against.Null(taskFound, nameof(taskFound),
                $"There is no a task available with this ID: {idTask}.");

            taskFound.SetStateTask(Enums.StateTask.Deleted);
            string query = $"UPDATE {_tableNameTasks} SET StateTask = @StateTask WHERE TaskID = @TaskID";
            var result = await connection.ExecuteAsync(query, taskFound);
            connection.Close();

            return result == 0 ? _mapper.Map<UpdateTaskDTO>(Guard.Against.Zero(result, nameof(result),
                                        $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateTaskDTO>(taskFound);
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(int idTask)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var taskFound = (from t in await connection.QueryAsync<Domain.Entities.Task>($"SELECT * FROM {_tableNameTasks}")
                             where t.TaskID == idTask && t.StateTask != Enums.StateTask.Deleted
                             select t)
                             .SingleOrDefault();
            connection.Close();

            Guard.Against.Null(taskFound, nameof(taskFound),
                $"There is no a task available with this ID: {idTask}.");

            return taskFound == null ? _mapper.Map<Domain.Entities.Task>(Guard.Against.Null(taskFound, nameof(taskFound),
                                           $"There is no a task available with this ID: {idTask}."))
                                        : _mapper.Map<Domain.Entities.Task>(taskFound);
        }

        public async Task<UpdateTaskDTO> UpdateTaskAsync(int idTask, Domain.Entities.Task task)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == task.ProjectID && p.StateProject == Enums.StateProject.Active
                                && p.Phase != Enums.Phase.Completed
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available or this is completed. ID: {task.ProjectID}.");

            var taskFound = (from t in await connection.QueryAsync<Domain.Entities.Task>($"SELECT * FROM {_tableNameTasks}")
                             where t.TaskID == idTask && t.StateTask == Enums.StateTask.Active
                             select t)
                             .SingleOrDefault();

            var taskToUpdate = taskFound != null ? TaskHandler.SetNewAplicableValuesToTaskEntity(taskFound, task)
                                                    : Guard.Against.Null(taskFound, nameof(taskFound),
                                                        $"There is no a task available with this ID: {idTask}.");

            Guard.Against.Null(taskToUpdate, nameof(taskToUpdate));
            Guard.Against.Null(taskToUpdate.TaskID, nameof(taskToUpdate.TaskID));
            Guard.Against.NullOrEmpty(taskToUpdate.ProjectID, nameof(taskToUpdate.ProjectID));
            Guard.Against.NullOrEmpty(taskToUpdate.Description, nameof(taskToUpdate.Description));
            Guard.Against.NullOrEmpty(taskToUpdate.CreatedBy, nameof(taskToUpdate.CreatedBy));
            Guard.Against.OutOfSQLDateRange(taskToUpdate.CreatedAt, nameof(taskToUpdate.CreatedAt));
            Guard.Against.OutOfSQLDateRange(taskToUpdate.DeadLine, nameof(taskToUpdate.DeadLine));
            Guard.Against.EnumOutOfRange(taskToUpdate.Priority, nameof(taskToUpdate.Priority));
            Guard.Against.EnumOutOfRange(taskToUpdate.StateTask, nameof(taskToUpdate.StateTask));

            var viableDeadlineDate = TaskHandler.ValidateDeadLineDate(taskToUpdate.DeadLine, projectFound);
            if (!viableDeadlineDate)
            {
                Guard.Against.Default(viableDeadlineDate, nameof(viableDeadlineDate),
                    $"Task deadline: {taskToUpdate.DeadLine:dd/MM/yyyy} " +
                    $"is greater than project deadline: {projectFound.DeadLine:dd/MM/yyyy} " +
                    $"or less than project open date: {projectFound.OpenDate:dd/MM/yyyy}.");
            }

            string query = $"UPDATE {_tableNameTasks} SET ProjectID = @ProjectID, Description = @Description, " +
                            $"CreatedBy = @CreatedBy, AssignedTo = @AssignedTo, CreatedAt = @CreatedAt, " +
                            $"AssignedAt = @AssignedAt, Deadline = @DeadLine, CompletedAt = @CompletedAt, " +
                            $"Priority = @Priority, StateTask = @StateTask WHERE TaskID = @TaskID";

            var result = await connection.ExecuteAsync(query, taskToUpdate);
            connection.Close();

            return result == 0 ? _mapper.Map<UpdateTaskDTO>(Guard.Against.Zero(result, nameof(result),
                                    $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<UpdateTaskDTO>(taskToUpdate);
        }
    }
}