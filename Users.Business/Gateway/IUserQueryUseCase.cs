using Users.Domain.Entities;

namespace Users.Business.Gateway
{
    public interface IUserQueryUseCase
    {
        Task<List<User>> GetUsersAsync();
        Task<List<User>> GetActiveUsersAsync();
        Task<User> GetUserByIdAsync(string uidUser);
    }
}
