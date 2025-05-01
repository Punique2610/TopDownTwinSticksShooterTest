using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IWeaponControllerComponent : IComponent
    {
        UnityAction OnWeaponEquipped { get; set; }

        IWeaponComponent GetCurrentWeapon();

        void Equip(IWeaponComponent weaponPrefab);
        void StartFiring();
        void StopFiring();
    }
}