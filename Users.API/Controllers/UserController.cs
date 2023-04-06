using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Business.Gateway;
using Users.Domain.Commands;
using Users.Domain.DTO;
using Users.Domain.Entities;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserQueryUseCase _userQueryUseCase;
        private readonly IUserCommandUseCase _userCommandUseCase;
        private readonly IMapper _mapper;

        public UserController(IUserQueryUseCase userUseCase, IUserCommandUseCase userCommandUseCase, IMapper mapper)
        {
            _userQueryUseCase = userUseCase;
            _userCommandUseCase = userCommandUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewUserDTO> CreateUserAsync([FromBody] NewUserCommand newUser)
        {
            return await _userCommandUseCase.CreateUserAsync(_mapper.Map<User>(newUser));
        }

        [HttpPut("ID")]
        public async Task<UpdateUserDTO> UpdateUserAsync(string uidUser, [FromBody] UpdateUserCommand updateUser)
        {
            return await _userCommandUseCase.UpdateUserAsync(uidUser, _mapper.Map<User>(updateUser));
        }

        [HttpDelete("ID")]
        public async Task<UpdateUserDTO> DeleteUserAsync(string uidUserd)
        {
            return await _userCommandUseCase.DeleteUserAsync(uidUserd);
        }

        [HttpGet("ID")]
        public async Task<User> GetUserByIdAsync(string uidUser)
        {
            return await _userQueryUseCase.GetUserByIdAsync(uidUser);
        }
    }
}