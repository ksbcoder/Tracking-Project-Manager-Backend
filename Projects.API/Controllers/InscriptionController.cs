using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Projects.Business.Gateway;
using Projects.Domain.Commands.Inscription;
using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;

namespace Projects.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InscriptionController : ControllerBase
    {
        private readonly IInscriptionUseCase _inscriptionUseCase;
        private readonly IMapper _mapper;

        public InscriptionController(IInscriptionUseCase inscriptionUseCase, IMapper mapper)
        {
            _inscriptionUseCase = inscriptionUseCase;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<NewInscriptionDTO> CreateInscriptionAsync([FromBody] NewInscriptionCommand newInscriptionCommand)
        {
            return await _inscriptionUseCase.CreateInscriptionAsync(_mapper.Map<Inscription>(newInscriptionCommand));
        }

        [HttpPut("ID")]
        public async Task<InscriptionRespondedDTO> RespondInscriptionAsync(string idInscription, int value)
        {
            return await _inscriptionUseCase.RespondInscriptionAsync(idInscription, value);
        }

        [HttpDelete("ID")]
        public async Task<Inscription> DeleteInscriptionAsync(string idInscription)
        {
            return await _inscriptionUseCase.DeleteInscriptionAsync(idInscription);
        }

        [HttpGet("NoResponded")]
        public async Task<List<Inscription>> GetInscriptionsNoRespondedAsync()
        {
            return await _inscriptionUseCase.GetInscriptionsNoRespondedAsync();
        }
    }
}