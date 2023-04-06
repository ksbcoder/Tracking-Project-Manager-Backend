using Users.Domain.Entities;

namespace Users.Business.Gateway
{
    public interface IUserQueryUseCase
    {
        Task<User> GetUserByIdAsync(string uidUser);
    }
}
