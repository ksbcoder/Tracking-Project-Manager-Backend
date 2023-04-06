﻿using Users.Business.Gateway;
using Users.Business.Gateway.Repositories.Queries;
using Users.Domain.Entities;

namespace Users.Business.UseCases.Queries
{
    public class UserQueryUseCase : IUserQueryUseCase
    {
        private readonly IUserQueryRepository _userQueryRepository;

        public UserQueryUseCase(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async Task<User> GetUserByIdAsync(string uidUser)
        {
            return await _userQueryRepository.GetUserByIdAsync(uidUser);
        }
    }
}