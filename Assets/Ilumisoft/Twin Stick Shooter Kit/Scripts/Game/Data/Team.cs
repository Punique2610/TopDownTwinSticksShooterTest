using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Simple data class allowing to define teams
    /// </summary>
    [CreateAssetMenu(menuName = "Affiliation/Team", fileName = "Team")]
    public class Team : ScriptableObject
    {
        public string ID;
    }
}