using Projects.Domain.Common;

namespace Projects.Domain.DTO.Inscription
{
    public class NewInscriptionDTO
    {
        public Guid ProjectID { get; set; }
        public string UidUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public Enums.StateInscription StateInscription { get; set; }
    }
}