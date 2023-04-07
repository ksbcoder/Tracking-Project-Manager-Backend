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
            return await _taskRepository.CreateTaskAsync(task);
        }

        public async Task<UpdateTaskDTO> DeleteTaskAsync(int idTask)
        {
            return await _taskRepository.DeleteTaskAsync(idTask);
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(int idTask)
        {
            return await _taskRepository.GetTaskByIdAsync(idTask);
        }

        public async Task<UpdateTaskDTO> UpdateTaskAsync(int idTask, Domain.Entities.Task task)
        {
            return await _taskRepository.UpdateTaskAsync(idTask, task);
        }
    }
}