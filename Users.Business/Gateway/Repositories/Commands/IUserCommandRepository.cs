using Users.Domain.DTO;
using Users.Domain.Entities;

namespace Users.Business.Gateway.Repositories.Commands
{
    public interface IUserCommandRepository
    {
        Task<NewUserDTO> CreateUserAsync(User user);
        Task<UpdateUserDTO> UpdateUserAsync(User user);
        Task<UpdateUserDTO> DeleteUserAsync(string uidUser);
    }
}
