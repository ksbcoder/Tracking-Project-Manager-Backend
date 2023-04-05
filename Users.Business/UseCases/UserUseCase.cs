using Users.Business.Gateway;
using Users.Business.Gateway.Repositories;
using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.Business.UseCases
{
    public class UserUseCase : IUserUseCase
    {
        private readonly IUserRepository _userRepository;
        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<NewUser> CreateUserAsync(User user)
        {
            return await _userRepository.CreateUserAsync(user);
        }

        public async Task<User> DeleteUserAsync(string uidUser)
        {
            return await _userRepository.DeleteUserAsync(uidUser);
        }

        public async Task<User> GetUserByIdAsync(string uidUser)
        {
            return await _userRepository.GetUserByIdAsync(uidUser);
        }

        public async Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser)
        {
            return await _userRepository.GetUsersByIncriptionIdAsync(uidUser);
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
