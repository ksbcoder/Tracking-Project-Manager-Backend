using Ardalis.GuardClauses;
using AutoMapper;
using MongoDB.Driver;
using Users.Business.Gateway.Repositories;
using Users.Domain.Commands;
using Users.Domain.Common;
using Users.Domain.Entities;
using Users.Infrastructure.Entities;
using Users.Infrastructure.Interfaces;

namespace Users.Infrastructure.Repositories
{
    public class WorkTeamRepository : IWorkTeamRepository
    {
        private readonly IMongoCollection<WorkTeamMongo> _workTeamsCollection;
        private readonly IMapper _mapper;

        public WorkTeamRepository(IContext context, IMapper mapper)
        {
            _workTeamsCollection = context.WorkTeams;
            _mapper = mapper;
        }

        public async Task<NewWorkTeam> CreateWorkTeamAsync()
        {
            var teamToCreate = WorkTeam.Create();
            Guard.Against.Null(teamToCreate, nameof(teamToCreate));
            Guard.Against.OutOfRange(teamToCreate.EfficiencyRate, nameof(teamToCreate.EfficiencyRate), 0, 100);
            Guard.Against.EnumOutOfRange(teamToCreate.StateTeam, nameof(teamToCreate.StateTeam));

            await _workTeamsCollection.InsertOneAsync(_mapper.Map<WorkTeamMongo>(teamToCreate));
            return _mapper.Map<NewWorkTeam>(teamToCreate);
        }

        public async Task<WorkTeam> DeleteWorkTeamAsync(string id)
        {
            var teamToDelete = await _workTeamsCollection.Find(wt => wt.TeamID == id 
                    && wt.StateTeam == Enums.StateTeam.Active 
                    || wt.StateTeam == Enums.StateTeam.Inactive).FirstOrDefaultAsync();

            if (Guard.Against.Null(teamToDelete, nameof(teamToDelete)) != null)
            {
                teamToDelete.SetStateTeam(Enums.StateTeam.Eliminated);
                 await _workTeamsCollection.FindOneAndReplaceAsync(wt => wt.TeamID == id ,teamToDelete);
            }

            return _mapper.Map<WorkTeam>(await _workTeamsCollection.Find(wt => wt.TeamID == id).FirstOrDefaultAsync());
        }

        public async Task<WorkTeam> UpdateWorkTeamAsync(string id, WorkTeam workTeam)
        {
            var teamToUpdate = _mapper.Map<WorkTeamMongo>(workTeam);
            teamToUpdate.SetTeamID(id);
            var teamUpdated = await _workTeamsCollection.FindOneAndReplaceAsync(wt => wt.TeamID == id
                    && wt.StateTeam == Enums.StateTeam.Active, teamToUpdate);

            return teamUpdated == null
                ? _mapper.Map<WorkTeam>(Guard.Against.Null(teamUpdated, nameof(teamUpdated)))
                : _mapper.Map<WorkTeam>(
                    await _workTeamsCollection.Find(wt => wt.TeamID == teamToUpdate.TeamID).FirstOrDefaultAsync());
        }
    }
}
