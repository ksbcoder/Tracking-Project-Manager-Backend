using Ardalis.GuardClauses;
using AutoMapper;
using Dapper;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.Common;
using Projects.Domain.Common.Handlers;
using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;
using Projects.Infrastructure.Gateway;

namespace Projects.Infrastructure.Repositories
{
    public class InscriptionRepository : IInscriptionRepository
    {
        private readonly IDbConnectionBuilder _dbConnectionBuilder;
        private readonly string _tableNameProjects = "Projects";
        private readonly string _tableNameInscriptions = "Inscriptions";
        private readonly IMapper _mapper;

        public InscriptionRepository(IDbConnectionBuilder dbConnectionBuilder, IMapper mapper)
        {
            _dbConnectionBuilder = dbConnectionBuilder;
            _mapper = mapper;
        }

        public async Task<NewInscriptionDTO> CreateInscriptionAsync(Inscription inscription)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var projectFound = (from p in await connection.QueryAsync<Project>($"SELECT * FROM {_tableNameProjects}")
                                where p.ProjectID == inscription.ProjectID && p.StateProject == Enums.StateProject.Active
                                && p.Phase != Enums.Phase.Completed
                                select p)
                                .SingleOrDefault();

            Guard.Against.Null(projectFound, nameof(projectFound),
                $"There is no a project available with ID: {inscription.ProjectID}.");

            Inscription.SetDetailsInscriptionEntity(inscription);
            var viableInscriptionDate = DatesHandler.ValidateWithinTheProjectDeadLineNotOpen(inscription.CreatedAt, projectFound);
            if (!viableInscriptionDate)
            {
                Guard.Against.Default(viableInscriptionDate, nameof(viableInscriptionDate),
                    $"Assign date: {inscription.CreatedAt:dd/MM/yyyy} " +
                    $"is greater than project deadline: {projectFound.DeadLine:dd/MM/yyyy} " +
                    $"or less than project open date: {projectFound.OpenDate:dd/MM/yyyy}.");
            }

            string query = $"INSERT INTO {_tableNameInscriptions} (ProjectID, UidUser, CreatedAt, ResponsedAt, " +
                            $"StateInscription) VALUES (@ProjectID, @UidUser, @CreatedAt, @ResponsedAt, " +
                            $"@StateInscription)";

            var result = await connection.ExecuteAsync(query, inscription);
            connection.Close();
            return result == 0 ? _mapper.Map<NewInscriptionDTO>(Guard.Against.Zero(result, nameof(result),
                                     $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<NewInscriptionDTO>(inscription);
        }

        public async Task<Inscription> DeleteInscriptionAsync(string idInscription)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var inscriptionFound = (from i in await connection.QueryAsync<Inscription>($"SELECT * FROM {_tableNameInscriptions}")
                                    where i.InscriptionID == Guid.Parse(idInscription)
                                    && i.StateInscription != Enums.StateInscription.Deleted
                                    select i)
                                    .SingleOrDefault();

            Guard.Against.Null(inscriptionFound, nameof(inscriptionFound),
                                   $"There is no a inscription available or was deleted already. ID: {idInscription}.");

            inscriptionFound.SetStateInscription(Enums.StateInscription.Deleted);
            string query = $"UPDATE {_tableNameInscriptions} SET StateInscription = @StateInscription " +
                            $"WHERE InscriptionID = @InscriptionID";
            var result = await connection.ExecuteAsync(query, inscriptionFound);
            connection.Close();

            return result == 0 ? _mapper.Map<Inscription>(Guard.Against.Zero(result, nameof(result),
                                    $"The record has not been modified. Rows affected ({result})"))
                                : inscriptionFound;
        }

        public async Task<List<Inscription>> GetInscriptionsNoRespondedAsync()
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var inscriptions = (from i in await connection.QueryAsync<Inscription>($"SELECT * FROM {_tableNameInscriptions}")
                                where i.StateInscription == Enums.StateInscription.Pending
                                select i)
                                .ToList();
            connection.Close();
            return inscriptions.Count == 0 ? _mapper.Map<List<Inscription>>(
                                                Guard.Against.NullOrEmpty(inscriptions, nameof(inscriptions),
                                                $"There are no inscriptions available."))
                                            : inscriptions;
        }

        public async Task<InscriptionRespondedDTO> RespondInscriptionAsync(string idInscription, int value)
        {
            var connection = await _dbConnectionBuilder.CreateConnectionAsync();

            var inscriptionFound = (from i in await connection.QueryAsync<Inscription>($"SELECT * FROM {_tableNameInscriptions}")
                                    where i.InscriptionID == Guid.Parse(idInscription)
                                    && i.StateInscription == Enums.StateInscription.Pending
                                    select i)
                                    .SingleOrDefault();

            Guard.Against.Null(inscriptionFound, nameof(inscriptionFound),
                           $"There is no a inscription available or was respond already. ID: {idInscription}.");
            Guard.Against.OutOfRange(value, nameof(value), 1, 2,
                           $"The value: {value} is out of range (1-2).");
            switch (value)
            {
                case 1:
                    inscriptionFound.SetStateInscription(Enums.StateInscription.Approved);
                    break;
                case 2:
                    inscriptionFound.SetStateInscription(Enums.StateInscription.Denied);
                    break;
            }
            inscriptionFound.SetResponsedAt(DateTime.Now);

            string query = $"UPDATE {_tableNameInscriptions} SET StateInscription = @StateInscription, " +
                            $"ResponsedAt = @ResponsedAt WHERE InscriptionID = @InscriptionID";
            var result = await connection.ExecuteAsync(query, inscriptionFound);
            connection.Close();

            return result == 0 ? _mapper.Map<InscriptionRespondedDTO>(Guard.Against.Zero(result, nameof(result),
                                               $"The record has not been modified. Rows affected ({result})"))
                                : _mapper.Map<InscriptionRespondedDTO>(inscriptionFound);
        }
    }
}
