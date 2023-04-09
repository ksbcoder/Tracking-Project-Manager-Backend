using Moq;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Commands.Inscription;
using Projects.Domain.Common;
using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;

namespace Projects.Tests.ProjectTests
{
    public class InscriptionRepositoryTest
    {
        private readonly Mock<IInscriptionRepository> _inscriptionRepositoryMock;

        public InscriptionRepositoryTest()
        {
            _inscriptionRepositoryMock = new();
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Create_Inscription_When_Object_Is_Ok()
        {
            //Arrange
            var inscriptionToCreate = new Inscription();
            inscriptionToCreate.SetProjectID(Guid.NewGuid());
            inscriptionToCreate.SetUidUser("ID");

            var inscriptionCreated = new NewInscriptionDTO
            {
                ProjectID = inscriptionToCreate.ProjectID,
                UidUser = inscriptionToCreate.UidUser,
                CreatedAt = DateTime.Now,
                StateInscription = Enums.StateInscription.Pending
            };

            _inscriptionRepositoryMock.Setup(m => m.CreateInscriptionAsync(inscriptionToCreate)).ReturnsAsync(inscriptionCreated);

            //Act
            var result = await _inscriptionRepositoryMock.Object.CreateInscriptionAsync(inscriptionToCreate);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<NewInscriptionDTO>(result);
            Assert.Equal(inscriptionCreated, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Delete_Inscription_When_InscriptionID_Exist_In_DB()
        {
            //Arrange
            string idInscription = Guid.NewGuid().ToString();

            var inscriptionDeleted = new Inscription();
            inscriptionDeleted.SetInscriptionID(Guid.Parse(idInscription));
            inscriptionDeleted.SetProjectID(Guid.NewGuid());
            inscriptionDeleted.SetUidUser("ID");

            _inscriptionRepositoryMock.Setup(m => m.DeleteInscriptionAsync(idInscription)).ReturnsAsync(inscriptionDeleted);

            //Act
            var result = await _inscriptionRepositoryMock.Object.DeleteInscriptionAsync(idInscription);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Inscription>(result);
            Assert.Equal(inscriptionDeleted, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Get_Inscriptions_No_Responded()
        {
            //Arrange
            var inscriptionFound = new Inscription();
            inscriptionFound.SetInscriptionID(Guid.NewGuid());
            inscriptionFound.SetProjectID(Guid.NewGuid());
            inscriptionFound.SetUidUser("ID");
            inscriptionFound.SetCreatedAt(DateTime.Now);
            inscriptionFound.SetResponsedAt(DateTime.Now);
            inscriptionFound.SetStateInscription(Enums.StateInscription.Pending);

            var inscriptionsList = new List<Inscription> { inscriptionFound };

            _inscriptionRepositoryMock.Setup(m => m.GetInscriptionsNoRespondedAsync()).ReturnsAsync(inscriptionsList);

            //Act
            var result = await _inscriptionRepositoryMock.Object.GetInscriptionsNoRespondedAsync();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<Inscription>>(result);
            Assert.Equal(inscriptionsList, result);
        }

        [Fact]
        public async System.Threading.Tasks.Task Test_Respond_Inscription()
        {
            //Arrange
            string idInscription = Guid.NewGuid().ToString();
            Enums.StateInscription state = Enums.StateInscription.Approved;

            var inscriptionResponded = new InscriptionRespondedDTO
            {
                ProjectID = Guid.NewGuid(),
                UidUser = "ID",
                CreatedAt = DateTime.Now,
                ResponsedAt = DateTime.Now,
                StateInscription = state
            };

            _inscriptionRepositoryMock.Setup(m => m.RespondInscriptionAsync(idInscription, (int)state)).ReturnsAsync(inscriptionResponded);

            //Act
            var result = await _inscriptionRepositoryMock.Object.RespondInscriptionAsync(idInscription, (int)state);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InscriptionRespondedDTO>(result);
            Assert.Equal(inscriptionResponded, result);
        }
    }
}