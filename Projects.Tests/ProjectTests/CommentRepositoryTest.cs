using Moq;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.DTO.Comment;
using Projects.Domain.Entities;

namespace Projects.Tests.ProjectTests
{
    public class CommentRepositoryTest
    {
        private readonly Mock<ICommentRepository> _commentRepositoryMock;

        public CommentRepositoryTest()
        {
            _commentRepositoryMock = new();
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Create_Comment_When_Object_Is_Ok()
        {
            //Arrange
            var commentToCreate = new Comment();
            commentToCreate.SetUidUser("ID");
            commentToCreate.SetProjectID(Guid.NewGuid());
            commentToCreate.SetDescription("Description");

            var commentCreated = new NewCommentDTO
            {
                UidUser = commentToCreate.UidUser,
                ProjectID = commentToCreate.ProjectID,
                Description = commentToCreate.Description,
                CreatedAt = DateTime.Now,
                StateComment = Enums.StateComment.Active
            };

            _commentRepositoryMock.Setup(m => m.CreateCommentAsync(commentToCreate)).ReturnsAsync(commentCreated);

            //Act
            var result = await _commentRepositoryMock.Object.CreateCommentAsync(commentToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NewCommentDTO>(result);
            Assert.Equal(commentCreated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Delete_Comment_When_CommentID_Exist_In_DB()
        {
            //Arrange
            int idComment = 1;
            string idUser = "ID";

            var commentDeleted = new UpdateCommentDTO
            {
                CommentID = idComment,
                UidUser = "ID",
                ProjectID = Guid.NewGuid(),
                Description = "Description",
                CreatedAt = DateTime.Now,
                StateComment = Enums.StateComment.Deleted
            };

            _commentRepositoryMock.Setup(m => m.DeleteCommentAsync(idComment, idUser)).ReturnsAsync(commentDeleted);
            //Act
            var result = await _commentRepositoryMock.Object.DeleteCommentAsync(idComment, idUser);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<UpdateCommentDTO>(result);
            Assert.Equal(commentDeleted, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_All_Comments()
        {
            //Arrange
            string idProject = "ID";
            var commentFound = new Comment();
            commentFound.SetUidUser("ID");
            commentFound.SetProjectID(Guid.NewGuid());
            commentFound.SetDescription("Description");

            var commentList = new List<Comment> { commentFound };

            _commentRepositoryMock.Setup(m => m.GetAllCommentsByProjectIdAsync(idProject)).ReturnsAsync(commentList);

            //Act
            var result = await _commentRepositoryMock.Object.GetAllCommentsByProjectIdAsync(idProject);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Comment>>(result);
            Assert.Equal(commentList, result);
        }
    }
}