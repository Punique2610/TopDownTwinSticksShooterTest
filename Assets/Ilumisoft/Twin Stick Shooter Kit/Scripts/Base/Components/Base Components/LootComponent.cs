using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the loot component
    /// </summary>
    public abstract class LootComponent : MonoBehaviour, ILootComponent
    {
        public GameObject GameObject => gameObject;

        public abstract void Drop();
    }
}