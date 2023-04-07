using Moq;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.DTO.Project;
using Projects.Domain.Entities;

namespace Projects.Tests.ProjectTests
{
    public class ProjectRepositoryTest
    {
        private readonly Mock<IProjectRepository> _mockProjectRepository;

        public ProjectRepositoryTest()
        {
            _mockProjectRepository = new Mock<IProjectRepository>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Create_Project_When_Object_Is_Ok()
        {
            //Arrange
            var projectToCreate = new Project();
            projectToCreate.SetLeaderID("ID");
            projectToCreate.SetName("Name");
            projectToCreate.SetDescription("Description");

            var projectCreated = new NewProjectDTO
            {
                LeaderID = "ID",
                Name = "Name",
                Description = "Description",
                CreatedAt = DateTime.Now,
                OpenDate = null,
                DeadLine = null,
                CompletedAt = null,
                EfficiencyRate = 0,
                Phase = null,
                StateProject = Enums.StateProject.Active
            };
            _mockProjectRepository.Setup(x => x.CreateProjectAsync(projectToCreate)).ReturnsAsync(projectCreated);

            //Act
            var result = await _mockProjectRepository.Object.CreateProjectAsync(projectToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectCreated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Delete_Project_When_ProjectID_Exist_In_DB()
        {
            //Arrange
            Guid projectToDelete = Guid.NewGuid();
            var projectDeleted = new UpdateProjectDTO
            {
                ProjectID = projectToDelete,
                LeaderID = "ID",
                Name = "Name",
                Description = "Description",
                CreatedAt = DateTime.Now,
                OpenDate = null,
                DeadLine = null,
                CompletedAt = null,
                EfficiencyRate = 0,
                Phase = null,
                StateProject = Enums.StateProject.Deleted
            };
            _mockProjectRepository.Setup(x => x.DeleteProjectAsync(projectToDelete.ToString("D"))).ReturnsAsync(projectDeleted);

            //Act
            var result = await _mockProjectRepository.Object.DeleteProjectAsync(projectToDelete.ToString("D"));

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectDeleted, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Update_Project_When_Object_Is_Ok()
        {
            //Arrange
            var projectToUpdate = new Project();
            projectToUpdate.SetProjectID(Guid.NewGuid());
            projectToUpdate.SetLeaderID("ID");
            projectToUpdate.SetName("Name");
            projectToUpdate.SetDescription("Description");

            var projectUpdated = new UpdateProjectDTO
            {
                ProjectID = projectToUpdate.ProjectID,
                LeaderID = "ID",
                Name = "Name",
                Description = "Description",
                CreatedAt = DateTime.Now,
                OpenDate = null,
                DeadLine = null,
                CompletedAt = null,
                EfficiencyRate = 0,
                Phase = null,
                StateProject = Enums.StateProject.Active
            };

            _mockProjectRepository.Setup(x => x.UpdateProjectAsync(
                projectToUpdate.ProjectID.ToString("D"), projectToUpdate)).ReturnsAsync(projectUpdated);

            //Act
            var result = await _mockProjectRepository.Object.UpdateProjectAsync(
                projectToUpdate.ProjectID.ToString("D"), projectToUpdate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectUpdated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_Project_By_ProjectID_When_ProjectID_Exist_In_DB()
        {
            //Arrange
            Guid projectID = Guid.NewGuid();
            var projectToFind = new Project();
            projectToFind.SetProjectID(projectID);

            var projectFound = new Project();
            projectFound.SetProjectID(projectID);
            projectFound.SetLeaderID("ID");
            projectFound.SetName("Name");
            projectFound.SetDescription("Description");
            projectFound.SetCreatedAt(DateTime.Now);
            projectFound.SetOpenDate(null);
            projectFound.SetDeadLine(null);
            projectFound.SetCompletedAt(null);
            projectFound.SetEfficiencyRate(0);
            projectFound.SetPhase(null);
            projectFound.SetStateProject(Enums.StateProject.Active);

            _mockProjectRepository.Setup(x => x.GetProjectByIdAsync(projectID.ToString("D"))).ReturnsAsync(projectFound);

            //Act
            var result = await _mockProjectRepository.Object.GetProjectByIdAsync(projectID.ToString("D"));

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectFound, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Open_Project_When_Status_Is_Active_And_Not_Yet_Opened()
        {
            //Arrange
            var projectID = Guid.NewGuid();
            var projectToOpen = new Project();
            projectToOpen.SetProjectID(projectID);
            projectToOpen.SetStateProject(Enums.StateProject.Active);
            projectToOpen.SetOpenDate(null);
            projectToOpen.SetDeadLine(null);
            projectToOpen.SetPhase(null);

            var projectOpened = new UpdateProjectDTO
            {
                ProjectID = projectID,
                LeaderID = "ID",
                Name = "Name",
                Description = "Description",
                CreatedAt = DateTime.Now,
                OpenDate = DateTime.Now,
                DeadLine = DateTime.Now,
                CompletedAt = null,
                EfficiencyRate = 0,
                Phase = Enums.Phase.Started,
                StateProject = Enums.StateProject.Active
            };
            _mockProjectRepository.Setup(m => m.OpenProjectAsync(projectID.ToString("D"), projectToOpen)).ReturnsAsync(projectOpened);

            //Act
            var result = await _mockProjectRepository.Object.OpenProjectAsync(projectID.ToString("D"), projectToOpen);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectOpened, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Complete_Project_When_Status_Is_Active_And_Not_Yet_Completed()
        {
            //Arrange
            Guid projectID = Guid.NewGuid();

            var projectCompleted = new UpdateProjectDTO
            {
                ProjectID = projectID,
                LeaderID = "ID",
                Name = "Name",
                Description = "Description",
                CreatedAt = DateTime.Now,
                OpenDate = DateTime.Now,
                DeadLine = DateTime.Now,
                CompletedAt = DateTime.Now,
                EfficiencyRate = 100,
                Phase = Enums.Phase.Completed,
                StateProject = Enums.StateProject.Inactive
            };

            _mockProjectRepository.Setup(m => m.CompleteProjectAsync(projectID.ToString("D"))).ReturnsAsync(projectCompleted);

            //Act
            var result = await _mockProjectRepository.Object.CompleteProjectAsync(projectID.ToString("D"));

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectCompleted, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_Active_Projects()
        {
            //Arrange
            var projectID = Guid.NewGuid();
            var project = new Project();
            project.SetProjectID(projectID);
            project.SetStateProject(Enums.StateProject.Active);
            project.SetOpenDate(null);
            project.SetDeadLine(null);
            project.SetPhase(null);

            var projectsList = new List<Project> { project };
            _mockProjectRepository.Setup(m => m.GetActiveProjectsAsync()).ReturnsAsync(projectsList);

            //Act
            var result = await _mockProjectRepository.Object.GetActiveProjectsAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(projectsList, result);
        }
    }
}