using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the hitbox component
    /// </summary>
    public abstract class HitboxComponent : MonoBehaviour, IHitboxComponent
    {
        public GameObject GameObject => gameObject;

        public abstract IHealthComponent Health { get; protected set; }

        public abstract void ApplyDamage(float damageAmount);
    }
}