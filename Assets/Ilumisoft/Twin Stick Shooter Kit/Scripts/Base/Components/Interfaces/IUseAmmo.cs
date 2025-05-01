using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IUseAmmo
    {
        int Ammo { get; set; }

        int MaxAmmo { get; set; }

        UnityAction OnAmmoAmountChanged { get; set; }

        void AddAmmo(int amount);
    }
}