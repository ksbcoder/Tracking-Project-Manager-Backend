using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projects.Business.Gateway;
using Projects.Domain.Commands.Task;
using Projects.Domain.DTO.Task;

namespace Projects.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskUseCase _taskUseCase;
        private readonly IMapper _mapper;

        public TaskController(ITaskUseCase taskUseCase, IMapper mapper)
        {
            _taskUseCase = taskUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewTaskDTO> CreateProjectAsync([FromBody] NewTaskCommand newTaskCommand)
        {
            return await _taskUseCase.CreateTaskAsync(_mapper.Map<Domain.Entities.Task>(newTaskCommand));
        }

        [HttpPut("ID")]
        public async Task<UpdateTaskDTO> UpdateTaskAsync(int idTask, [FromBody] UpdateTaskCommand updateTaskCommand)
        {
            return await _taskUseCase.UpdateTaskAsync(idTask, _mapper.Map<Domain.Entities.Task>(updateTaskCommand));
        }

        [HttpDelete("ID")]
        public async Task<UpdateTaskDTO> DeleteTaskAsync(int idTask)
        {
            return await _taskUseCase.DeleteTaskAsync(idTask);
        }

        [HttpGet("ID")]
        public async Task<Domain.Entities.Task> GetTaskByIdAsync(int idTask)
        {
            return await _taskUseCase.GetTaskByIdAsync(idTask);
        }

        [HttpGet("All")]
        public async Task<List<Domain.Entities.Task>> GetAllTasksAsync()
        {
            return await _taskUseCase.GetAllTasksAsync();
        }

        [HttpGet("Unassigned")]
        public async Task<List<Domain.Entities.Task>> GetUnassignedTasksAsync()
        {
            return await _taskUseCase.GetUnassignedTasksAsync();
        }

        [HttpPut("CompleteTask/ID")]
        public async Task<UpdateTaskDTO> CompleteTaskAsync(int idTask)
        {
            return await _taskUseCase.CompleteTaskAsync(idTask);
        }

        [HttpPut("AssignTask/ID")]
        public async Task<UpdateTaskDTO> AssignTaskAsync(int idTask, string uidUser)
        {
            return await _taskUseCase.AssignTaskAsync(idTask, uidUser);
        }
    }
}