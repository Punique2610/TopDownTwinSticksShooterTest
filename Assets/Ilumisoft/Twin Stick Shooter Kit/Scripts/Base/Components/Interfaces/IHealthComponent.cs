using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IHealthComponent : IComponent
    {
        bool IsAlive { get; }

        float CurrentHealth { get; }

        float MaxHealth { get; set; }

        UnityAction<float> OnHealthChanged { get; set; }

        UnityAction OnHealthEmpty { get; set; }

        void SetHealth(float health);

        void AddHealth(float amount);

        void ApplyDamage(float damage);
    }
}