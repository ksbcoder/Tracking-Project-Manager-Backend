using Users.Business.Gateway;
using Users.Business.Gateway.Repositories;
using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.UseCases
{
    public class WorkTeamUseCase : IWorkTeamUseCase
    {
        private readonly IWorkTeamRepository _workTeamRepository;
        public WorkTeamUseCase(IWorkTeamRepository workTeamRepository)
        {
            _workTeamRepository = workTeamRepository;
        }

        public async Task<NewWorkTeam> CreateWorkTeamAsync()
        {
            return await _workTeamRepository.CreateWorkTeamAsync();
        }

        public async Task<WorkTeam> DeleteWorkTeamAsync(string id)
        {
            return await _workTeamRepository.DeleteWorkTeamAsync(id);
        }

        public async Task<WorkTeam> UpdateWorkTeamAsync(string id, WorkTeam workTeam)
        {
            return await _workTeamRepository.UpdateWorkTeamAsync(id, workTeam);
        }
    }
}
