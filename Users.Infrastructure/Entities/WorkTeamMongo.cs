using MongoDB.Bson.Serialization.Attributes;
using Users.Domain.Common;

namespace Users.Infrastructure.Entities
{
    public class WorkTeamMongo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string TeamID { get; private set; }

        public decimal EfficiencyRate { get; private set; }
        public Enums.StateTeam StateTeam { get; private set; }

        #region Setters
        public void SetTeamID(string newTeamID)
        {
            TeamID = newTeamID;
        }
        public void SetEfficiencyRate(decimal newEfficiencyRate)
        {
            EfficiencyRate = newEfficiencyRate;
        }
        public void SetStateTeam(Enums.StateTeam newStateTeam)
        {
            StateTeam = newStateTeam;
        }
        #endregion
    }
}
