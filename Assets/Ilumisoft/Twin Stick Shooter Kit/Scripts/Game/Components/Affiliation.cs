using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the affiliation component. 
    /// It allows you to define which team an actor belongs to.
    /// </summary>
    public class Affiliation : AffiliationComponent
    {
        [SerializeField]
        private Team team;

        public override string GetTeamID()
        {
            if (team == null)
            {
                return string.Empty;
            }

            return team.ID;
        }
    }
}