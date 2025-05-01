using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the hitbox component.
    /// It allows you to define a damage multiplier in the inspector.
    /// In that way you can create multiple hitboxes on an actor for different damage zones (head, body, arms,...)
    /// </summary>
    [RequireComponent(typeof(Collider))]
    [AddComponentMenu("Twin Stick Shooter Kit/Game/Hitbox")]
    public class Hitbox : HitboxComponent
    {
        [Range(0f, 1f)]
        public float damageMultiplier = 1.0f;

        public override IHealthComponent Health { get; protected set; }

        private void Awake()
        {
            Health = GetComponentInParent<IHealthComponent>();
        }

        public override void ApplyDamage(float damageAmount)
        {
            if (isActiveAndEnabled && !Health.IsNullOrDestroyed())
            {
                Health.ApplyDamage(damageAmount * damageMultiplier);
            }
        }
    }
}