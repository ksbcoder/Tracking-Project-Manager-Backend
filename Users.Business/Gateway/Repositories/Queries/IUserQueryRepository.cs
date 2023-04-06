using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.Gateway.Repositories.Queries
{
    public interface IUserQueryRepository
    {
        Task<User> GetUserByIdAsync(string uidUser);
        Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser);
    }
}
