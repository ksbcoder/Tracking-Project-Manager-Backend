using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.Gateway.Repositories
{
    public interface IWorkTeamRepository
    {
        Task<NewWorkTeam> CreateWorkTeamAsync();
        Task<WorkTeam> UpdateWorkTeamAsync(string id, WorkTeam workTeam);
        Task<WorkTeam> DeleteWorkTeamAsync(string id);
    }
}
