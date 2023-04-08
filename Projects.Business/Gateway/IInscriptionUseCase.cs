using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway
{
    public interface IInscriptionUseCase
    {
        Task<NewInscriptionDTO> CreateInscriptionAsync(Inscription inscription);
        Task<InscriptionRespondedDTO> RespondInscriptionAsync(string idInscription, int value);
        Task<List<Inscription>> GetInscriptionsNoRespondedAsync();
        Task<Inscription> DeleteInscriptionAsync(string idInscription);
    }
}