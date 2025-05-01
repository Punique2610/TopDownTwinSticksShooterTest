using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Default implementation for the loot component. 
    /// It allows you to define a loot object in the inspector, that can be dropped (e.g. when OnHealthEmpty gets invoked).
    /// </summary>
    [AddComponentMenu("Twin Stick Shooter Kit/Game/Loot")]
    public class Loot : LootComponent
    {
        /// <summary>
        /// Prefab for the element that will be dropped
        /// </summary>
        [SerializeField]
        private GameObject prefab;

        /// <summary>
        /// Drops the loot
        /// </summary>
        public override void Drop()
        {
            if (prefab != null)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
    }
}