﻿using Projects.Domain.DTO.Task;

namespace Projects.Business.Gateway.Repositories
{
    public interface ITaskRepository
    {
        Task<NewTaskDTO> CreateTaskAsync(Domain.Entities.Task task);
        Task<UpdateTaskDTO> UpdateTaskAsync(int idTask, Domain.Entities.Task task);
        Task<UpdateTaskDTO> DeleteTaskAsync(int idTask);
        Task<Domain.Entities.Task> GetTaskByIdAsync(int idTask);
        Task<List<Domain.Entities.Task>> GetUnassignedTasksAsync();
        Task<List<Domain.Entities.Task>> GetAllTasksAsync();
        //use cases
        Task<UpdateTaskDTO> AssignTaskAsync(int idTask, string uidUser);
        Task<UpdateTaskDTO> CompleteTaskAsync(int idTask);
    }
}