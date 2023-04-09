using Moq;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.DTO.Task;

namespace Projects.Tests.ProjectTests
{
    public class TaskRepositoryTest
    {
        private readonly Mock<ITaskRepository> _taskRepositoryMock;

        public TaskRepositoryTest()
        {
            _taskRepositoryMock = new();
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Create_Task_When_Object_Is_Ok()
        {
            //Arrange
            var taskToCreate = new Domain.Entities.Task();
            taskToCreate.SetProjectID(Guid.NewGuid());
            taskToCreate.SetDescription("Description");
            taskToCreate.SetCreatedBy("ID");
            taskToCreate.SetDeadLine(DateTime.Now);
            taskToCreate.SetPriority(Enums.Priority.Medium);

            var taskCreated = new NewTaskDTO
            {
                ProjectID = taskToCreate.ProjectID,
                Description = taskToCreate.Description,
                CreatedBy = taskToCreate.CreatedBy,
                AssignedTo = null,
                CreatedAt = DateTime.Now,
                AssignedAt = null,
                Deadline = taskToCreate.DeadLine,
                CompletedAt = null,
                Priority = taskToCreate.Priority,
                StateTask = Enums.StateTask.Active
            };
            _taskRepositoryMock.Setup(m => m.CreateTaskAsync(taskToCreate)).ReturnsAsync(taskCreated);

            //Act
            var result = await _taskRepositoryMock.Object.CreateTaskAsync(taskToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskCreated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Delete_Task_When_ProjectID_Exist_In_DB()
        {
            //Arrange
            int taskID = 1;

            var taskDeleted = new UpdateTaskDTO
            {
                TaskID = taskID,
                ProjectID = Guid.NewGuid(),
                Description = "Description",
                CreatedBy = "ID",
                AssignedTo = null,
                CreatedAt = DateTime.Now,
                AssignedAt = null,
                Deadline = DateTime.Now,
                CompletedAt = null,
                Priority = Enums.Priority.Medium,
                StateTask = Enums.StateTask.Deleted
            };
            _taskRepositoryMock.Setup(m => m.DeleteTaskAsync(taskID)).ReturnsAsync(taskDeleted);

            //Act
            var result = await _taskRepositoryMock.Object.DeleteTaskAsync(taskID);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskDeleted, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Update_Task_When_Object_Is_Ok()
        {
            //Arrange
            int taskID = 1;

            var taskToUpdate = new Domain.Entities.Task();
            taskToUpdate.SetProjectID(Guid.NewGuid());
            taskToUpdate.SetDescription("Description");
            taskToUpdate.SetAssignedTo("ID");
            taskToUpdate.SetAssignedAt(DateTime.Now);
            taskToUpdate.SetDeadLine(DateTime.Now);
            taskToUpdate.SetPriority(Enums.Priority.Medium);

            var taskUpdated = new UpdateTaskDTO
            {
                TaskID = taskID,
                ProjectID = taskToUpdate.ProjectID,
                Description = taskToUpdate.Description,
                CreatedBy = "ID",
                AssignedTo = taskToUpdate.AssignedTo,
                CreatedAt = DateTime.Now,
                AssignedAt = taskToUpdate.AssignedAt,
                Deadline = taskToUpdate.DeadLine,
                CompletedAt = null,
                Priority = taskToUpdate.Priority,
                StateTask = Enums.StateTask.Assigned
            };
            _taskRepositoryMock.Setup(m => m.UpdateTaskAsync(taskID, taskToUpdate)).ReturnsAsync(taskUpdated);

            //Act
            var result = await _taskRepositoryMock.Object.UpdateTaskAsync(taskID, taskToUpdate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskUpdated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_Task_By_TaskID_When_Exist_In_DB()
        {
            //Arrange
            var taskID = 1;
            var taskToFind = new Domain.Entities.Task();
            taskToFind.SetTaskID(taskID);

            var taskFound = new Domain.Entities.Task();
            taskFound.SetProjectID(Guid.NewGuid());
            taskFound.SetDescription("Description");
            taskFound.SetCreatedBy("ID");
            taskFound.SetAssignedTo("ID");
            taskFound.SetCreatedAt(DateTime.Now);
            taskFound.SetAssignedAt(DateTime.Now);
            taskFound.SetDeadLine(DateTime.Now);
            taskFound.SetCompletedAt(DateTime.Now);
            taskFound.SetPriority(Enums.Priority.Medium);
            taskFound.SetStateTask(Enums.StateTask.Completed);

            _taskRepositoryMock.Setup(m => m.GetTaskByIdAsync(taskID)).ReturnsAsync(taskFound);

            //Act
            var result = await _taskRepositoryMock.Object.GetTaskByIdAsync(taskID);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskFound, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_All_Tasks()
        {
            //Arrange
            var taskToFind = new Domain.Entities.Task();
            taskToFind.SetProjectID(Guid.NewGuid());
            taskToFind.SetDescription("Description");
            taskToFind.SetCreatedBy("ID");
            taskToFind.SetDeadLine(DateTime.Now);
            taskToFind.SetPriority(Enums.Priority.Medium);

            var taskList = new List<Domain.Entities.Task> { taskToFind };
            _taskRepositoryMock.Setup(m => m.GetAllTasksAsync()).ReturnsAsync(taskList);

            //Act
            var result = await _taskRepositoryMock.Object.GetAllTasksAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskList, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_Unassigned_Tasks()
        {
            //Arrange
            var taskToFind = new Domain.Entities.Task();
            taskToFind.SetProjectID(Guid.NewGuid());
            taskToFind.SetDescription("Description");
            taskToFind.SetCreatedBy("ID");
            taskToFind.SetAssignedTo(null);
            taskToFind.SetDeadLine(DateTime.Now);
            taskToFind.SetPriority(Enums.Priority.Medium);
            taskToFind.SetStateTask(Enums.StateTask.Active);

            var taskList = new List<Domain.Entities.Task> { taskToFind };
            _taskRepositoryMock.Setup(m => m.GetUnassignedTasksAsync()).ReturnsAsync(taskList);

            //Act
            var result = await _taskRepositoryMock.Object.GetUnassignedTasksAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskList, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Assign_Task_When_Status_Is_Active_And_Not_Yet_Assigned()
        {
            //Arrange
            var taskID = 1;
            var uidUser = "ID";

            var taskAssigned = new UpdateTaskDTO
            {
                TaskID = taskID,
                ProjectID = Guid.NewGuid(),
                Description = "Description",
                CreatedBy = uidUser,
                AssignedTo = null,
                CreatedAt = DateTime.Now,
                AssignedAt = DateTime.Now,
                Deadline = DateTime.Now,
                CompletedAt = null,
                Priority = Enums.Priority.Medium,
                StateTask = Enums.StateTask.Assigned
            };
            _taskRepositoryMock.Setup(m => m.AssignTaskAsync(taskID, uidUser)).ReturnsAsync(taskAssigned);

            //Act
            var result = await _taskRepositoryMock.Object.AssignTaskAsync(taskID, uidUser);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskAssigned, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Complete_Task_When_Status_Is_Assigned_And_Not_Yet_Completed()
        {
            //Arrange
            var taskID = 1;

            var taskCompleted = new UpdateTaskDTO
            {
                TaskID = taskID,
                ProjectID = Guid.NewGuid(),
                Description = "Description",
                CreatedBy = "ID",
                AssignedTo = "ID",
                CreatedAt = DateTime.Now,
                AssignedAt = DateTime.Now,
                Deadline = DateTime.Now,
                CompletedAt = DateTime.Now,
                Priority = Enums.Priority.Medium,
                StateTask = Enums.StateTask.Completed
            };
            _taskRepositoryMock.Setup(m => m.CompleteTaskAsync(taskID)).ReturnsAsync(taskCompleted);

            //Act
            var result = await _taskRepositoryMock.Object.CompleteTaskAsync(taskID);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(taskCompleted, result);

        }
    }
}
