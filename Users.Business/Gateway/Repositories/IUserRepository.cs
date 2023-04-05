using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.Gateway.Repositories
{
    public interface IUserRepository
    {
        Task<NewUser> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task<User> DeleteUserAsync(string uidUser);
        Task<User> GetUserByIdAsync(string uidUser);
        Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser);
    }
}
