using Users.Business.Gateway;
using Users.Business.Gateway.Repositories.Commands;
using Users.Domain.DTO;
using Users.Domain.Entities;

namespace Users.Business.UseCases.Commands
{
    public class UserCommandUseCase : IUserCommandUseCase
    {

        private readonly IUserCommandRepository _userCommandRepository;

        public UserCommandUseCase(IUserCommandRepository userCommandRepository)
        {
            _userCommandRepository = userCommandRepository;
        }

        public async Task<NewUserDTO> CreateUserAsync(User user)
        {
            return await _userCommandRepository.CreateUserAsync(user);
        }

        public async Task<UpdateUserDTO> DeleteUserAsync(string uidUser)
        {
            return await _userCommandRepository.DeleteUserAsync(uidUser);
        }

        public async Task<UpdateUserDTO> UpdateUserAsync(User user)
        {
            return await _userCommandRepository.UpdateUserAsync(user);
        }
    }
}
