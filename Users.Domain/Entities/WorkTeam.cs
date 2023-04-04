using Users.Domain.Common;

namespace Users.Domain.Entities
{
    public class WorkTeam
    {
        public string TeamID { get; private set; }
        public decimal EfficiencyRate { get; private set; }
        public Enums.StateTeam StateTeam { get; private set; }

        public static WorkTeam Create()
        {
            return new WorkTeam
            {
                EfficiencyRate = 0,
                StateTeam = Enums.StateTeam.Active
            };
        }

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
