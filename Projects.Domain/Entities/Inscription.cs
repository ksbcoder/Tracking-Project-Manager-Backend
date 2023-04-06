using Projects.Domain.Common;

namespace Projects.Domain.Entities
{
    public class Inscription
    {
        public Guid InscriptionID { get; private set; }
        public Guid ProjectID { get; private set; }
        public string UidUser { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? ResponsedAt { get; private set; }
        public Enums.StateInscription StateInscription { get; private set; }

        public Inscription() { }

        #region setters
        public void SetInscriptionID(Guid inscriptionID)
        {
            InscriptionID = inscriptionID;
        }
        public void SetProjectID(Guid projectID)
        {
            ProjectID = projectID;
        }
        public void SetUidUser(string userID)
        {
            UidUser = userID;
        }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }
        public void SetResponsedAt(DateTime responsedAt)
        {
            ResponsedAt = responsedAt;
        }
        public void SetStateInscription(Enums.StateInscription stateInscription)
        {
            StateInscription = stateInscription;
        }
        #endregion
    }
}
