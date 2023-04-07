using Moq;
using Users.Business.Gateway.Repositories.Commands;
using Users.Business.Gateway.Repositories.Queries;
using Users.Domain.Common;
using Users.Domain.DTO;
using Users.Domain.Entities;

namespace Users.Tests.UserTests
{
    public class UserRepositoryTest
    {
        private readonly Mock<IUserQueryRepository> _mockUserQueryRepositoryMock;
        private readonly Mock<IUserCommandRepository> _mockUserCommandRepositoryMock;

        public UserRepositoryTest()
        {
            _mockUserQueryRepositoryMock = new();
            _mockUserCommandRepositoryMock = new();
        }

        [Fact]
        public async Task Test_Create_User_When_Object_Is_Ok()
        {
            //Arrange
            var userToCreate = new User();
            userToCreate.SetUidUser("ID");
            userToCreate.SetUserName("Name");
            userToCreate.SetEmail("Email");
            userToCreate.SetRole(Enums.Roles.Contributor);

            var userCreated = new NewUserDTO
            {
                UidUser = "ID",
                UserName = "Name",
                Email = "Email",
                EfficiencyRate = 100,
                TasksCompleted = 0,
                Role = Enums.Roles.Contributor,
                StateUser = Enums.StateUser.Active
            };
            _mockUserCommandRepositoryMock.Setup(m => m.CreateUserAsync(userToCreate)).ReturnsAsync(userCreated);

            //Act
            var result = await _mockUserCommandRepositoryMock.Object.CreateUserAsync(userToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userCreated, result);
        }

        [Fact]
        public async Task Test_Delete_User_When_UidUser_Exist_In_DB()
        {
            //Arrange
            string userToDelete = "ID";

            var userDeleted = new UpdateUserDTO
            {
                UidUser = "ID",
                UserName = "Name",
                Email = "Email",
                EfficiencyRate = 100,
                TasksCompleted = 0,
                Role = Enums.Roles.Contributor,
                StateUser = Enums.StateUser.Active
            };

            _mockUserCommandRepositoryMock.Setup(m => m.DeleteUserAsync(userToDelete)).ReturnsAsync(userDeleted);

            //Act
            var result = await _mockUserCommandRepositoryMock.Object.DeleteUserAsync(userToDelete);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userDeleted, result);
        }

        [Fact]
        public async Task Test_Get_User_By_Id_When_UidUser_Exist_In_DB()
        {
            //Arrange
            string userToGet = "ID";

            var userFound = new User();
            userFound.SetUidUser("ID");
            userFound.SetUserName("Name");
            userFound.SetEmail("Email");
            userFound.SetRole(Enums.Roles.Contributor);

            _mockUserQueryRepositoryMock.Setup(m => m.GetUserByIdAsync(userToGet)).ReturnsAsync(userFound);

            //Act
            var result = await _mockUserQueryRepositoryMock.Object.GetUserByIdAsync(userToGet);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userFound, result);
        }

        [Fact]
        public async Task Test_Update_User_When_UidUser_Exist_In_DB_And_Object_Is_Ok()
        {
            //Arrange
            string userToUpdateID = "ID";
            var userToUpdate = new User();
            userToUpdate.SetUserName("Name");
            userToUpdate.SetEmail("Email");
            userToUpdate.SetEfficiencyRate(85);
            userToUpdate.SetTasksCompleted(1);
            userToUpdate.SetRole(Enums.Roles.Contributor);
            userToUpdate.SetStateUser(Enums.StateUser.Active);

            var userUpdated = new UpdateUserDTO
            {
                UidUser = "ID",
                UserName = "Name",
                Email = "Email",
                EfficiencyRate = 85,
                TasksCompleted = 1,
                Role = Enums.Roles.Contributor,
                StateUser = Enums.StateUser.Active
            };

            _mockUserCommandRepositoryMock.Setup(m => m.UpdateUserAsync(userToUpdateID, userToUpdate)).ReturnsAsync(userUpdated);

            //Act
            var result = await _mockUserCommandRepositoryMock.Object.UpdateUserAsync(userToUpdateID, userToUpdate);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(userUpdated, result);
        }
    }
}
