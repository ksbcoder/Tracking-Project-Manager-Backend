using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Users.Business.Gateway;
using Users.Domain.Commands;
using Users.Domain.Entities;

namespace Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTeamController : ControllerBase
    {
        private readonly IWorkTeamUseCase _workTeamUseCase;
        private readonly IMapper _mapper;

        public WorkTeamController(IWorkTeamUseCase workTeamUseCase, IMapper mapper)
        {
            _workTeamUseCase = workTeamUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewWorkTeam> CreateWorkTeamAsync()
        {
            return await _workTeamUseCase.CreateWorkTeamAsync();
        }

        [HttpPut]
        public async Task<WorkTeam> UpdateWorkTeamAsync(string id, [FromBody] NewWorkTeam newWorkTeam)
        {
            return await _workTeamUseCase.UpdateWorkTeamAsync(id, _mapper.Map<WorkTeam>(newWorkTeam));
        }

        [HttpDelete("{id}")]
        public async Task<WorkTeam> DeleteWorkTeamAsync(string id)
        {
            return await _workTeamUseCase.DeleteWorkTeamAsync(id);
        }
    }
}
