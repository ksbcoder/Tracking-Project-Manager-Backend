using Projects.Business.Gateway;
using Projects.Business.Gateway.Repositories;
using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;

namespace Projects.Business.UseCases
{
    public class InscriptionUseCase : IInscriptionUseCase
    {
        private readonly IInscriptionRepository _inscriptionRepository;

        public InscriptionUseCase(IInscriptionRepository inscriptionRepository)
        {
            _inscriptionRepository = inscriptionRepository;
        }

        public async Task<NewInscriptionDTO> CreateInscriptionAsync(Inscription inscription)
        {
            return await _inscriptionRepository.CreateInscriptionAsync(inscription);
        }

        public async Task<Inscription> DeleteInscriptionAsync(string idInscription)
        {
            return await _inscriptionRepository.DeleteInscriptionAsync(idInscription);
        }

        public async Task<List<Inscription>> GetInscriptionsNoRespondedAsync()
        {
            return await _inscriptionRepository.GetInscriptionsNoRespondedAsync();
        }

        public async Task<InscriptionRespondedDTO> RespondInscriptionAsync(string idInscription, int value)
        {
            return await _inscriptionRepository.RespondInscriptionAsync(idInscription, value);
        }
    }
}