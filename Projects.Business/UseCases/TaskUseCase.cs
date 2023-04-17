using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO.Task;

namespace Projects.Business.UseCases
{
    public class TaskUseCase : ITaskUseCase
    {
        private readonly ITaskRepository _taskRepository;
        public TaskUseCase(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UpdateTaskDTO> AssignTaskAsync(int idTask, string uidUser)
        {
            return await _taskRepository.AssignTaskAsync(idTask, uidUser);
        }

        public async Task<UpdateTaskDTO> CompleteTaskAsync(int idTask)
        {
            return await _taskRepository.CompleteTaskAsync(idTask);
        }

        public async Task<NewTaskDTO> CreateTaskAsync(Domain.Entities.Task task)
        {
            return await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<UpdateTaskDTO> DeleteTaskAsync(int idTask)
        {
            return await _taskRepository.DeleteTaskAsync(idTask);
        }

        public async Task<List<Domain.Entities.Task>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(int idTask)
        {
            return await _taskRepository.GetTaskByIdAsync(idTask);
        }

        public async Task<List<Domain.Entities.Task>> GetTasksByUserIdAsync(string uidUser)
        {
            return await _taskRepository.GetTasksByUserIdAsync(uidUser);
        }

        public async Task<List<Domain.Entities.Task>> GetUnassignedTasksAsync(string idLeader)
        {
            return await _taskRepository.GetUnassignedTasksAsync(idLeader);
        }

        public async Task<UpdateTaskDTO> UpdateTaskAsync(int idTask, Domain.Entities.Task task)
        {
            return await _taskRepository.UpdateTaskAsync(idTask, task);
        }
    }
}