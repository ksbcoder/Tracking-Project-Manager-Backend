using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Business.Gateway;
using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserUseCase _userUseCase;
        private readonly IMapper _mapper;

        public UserController(IUserUseCase userUseCase, IMapper mapper)
        {
            _userUseCase = userUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewUser> CreateUserAsync([FromBody] NewUser newUser)
        {
            return await _userUseCase.CreateUserAsync(_mapper.Map<User>(newUser));
        }

        [HttpPut]
        public async Task<User> UpdateUserAsync([FromBody] NewUser newUser)
        {
            return await _userUseCase.UpdateUserAsync(_mapper.Map<User>(newUser));
        }

        [HttpDelete("ID")]
        public async Task<User> DeleteUserAsync(string uidUserd)
        {
            return await _userUseCase.DeleteUserAsync(uidUserd);
        }

        [HttpGet("ID")]
        public async Task<User> GetUserByIdAsync(string uidUser)
        {
            return await _userUseCase.GetUserByIdAsync(uidUser);
        }

        [HttpGet("IncriptionID")]
        public async Task<List<User>> GetUsersByIncriptionIdAsync(string uidUser)
        {
            return await _userUseCase.GetUsersByIncriptionIdAsync(uidUser);
        }
    }
}
