﻿using Users.Domain.DTO;
using Users.Domain.Entities;

namespace Users.Business.Gateway
{
    public interface IUserCommandUseCase
    {
        Task<NewUserDTO> CreateUserAsync(User user);
        Task<UpdateUserDTO> UpdateUserAsync(string uidUser, User user);
        Task<UpdateUserDTO> DeleteUserAsync(string uidUser);
    }
}
