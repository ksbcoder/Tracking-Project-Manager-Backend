using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.Gateway
{
    public interface IUserQueryUseCase
    {
        Task<User> GetUserByIdAsync(string uidUser);
        Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser);
    }
}
