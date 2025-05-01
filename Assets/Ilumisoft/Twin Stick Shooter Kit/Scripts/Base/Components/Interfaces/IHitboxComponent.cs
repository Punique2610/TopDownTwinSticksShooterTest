namespace Ilumisoft.TwinStickShooterKit
{
    public interface IHitboxComponent : IComponent
    {
        IHealthComponent Health { get; }

        void ApplyDamage(float damageAmount);
    }
}