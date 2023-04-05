using MongoDB.Bson.Serialization.Attributes;
using Users.Domain.Common;

namespace Users.Infrastructure.Entities
{
    public class UserMongo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserID { get; private set; }

        public string UidUser { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public decimal EfficiencyRate { get; private set; }
        public Enums.Roles Role { get; private set; }
        public Enums.StateUser StateUser { get; private set; }

        #region Setters
        public void SetUserID(string newUserID)
        {
            UidUser = newUserID;
        }
        public void SetUidUser(string uidUser)
        {
            UidUser = uidUser;
        }
        public void SetUserName(string newUserName)
        {
            UserName = newUserName;
        }
        public void SetEmail(string newEmail)
        {
            Email = newEmail;
        }
        public void SetEfficiencyRate(decimal newEfficiencyRate)
        {
            EfficiencyRate = newEfficiencyRate;
        }
        public void SetRole(Enums.Roles newRole)
        {
            Role = newRole;
        }
        public void SetStateUser(Enums.StateUser newStateUser)
        {
            StateUser = newStateUser;
        }
        #endregion
    }
}
