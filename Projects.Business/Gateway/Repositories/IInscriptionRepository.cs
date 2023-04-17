using Projects.Domain.DTO.Inscription;
using Projects.Domain.Entities;

namespace Projects.Business.Gateway.Repositories
{
    public interface IInscriptionRepository
    {
        Task<NewInscriptionDTO> CreateInscriptionAsync(Inscription inscription);
        Task<InscriptionRespondedDTO> RespondInscriptionAsync(string idInscription, int value);
        Task<List<Inscription>> GetInscriptionsNoRespondedAsync();
        Task<Inscription> GetInscriptionByUserIdAsync(string idUser);
        Task<Inscription> DeleteInscriptionAsync(string idInscription);
    }
}