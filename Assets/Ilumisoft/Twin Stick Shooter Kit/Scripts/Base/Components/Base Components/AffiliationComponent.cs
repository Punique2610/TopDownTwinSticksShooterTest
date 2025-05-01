using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class of the affiliation component
    /// </summary>
    public abstract class AffiliationComponent : MonoBehaviour, IAffiliationComponent
    {
        public GameObject GameObject => gameObject;

        public abstract string GetTeamID();
    }
}