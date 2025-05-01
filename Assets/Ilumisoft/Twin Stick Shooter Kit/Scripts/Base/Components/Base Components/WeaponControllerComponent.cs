using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the weapon controller component
    /// </summary>
    public abstract class WeaponControllerComponent : MonoBehaviour, IWeaponControllerComponent
    {
        public GameObject GameObject => gameObject;
        public UnityAction OnWeaponEquipped { get; set; }
        public abstract void Equip(IWeaponComponent weaponPrefab);
        public abstract IWeaponComponent GetCurrentWeapon();
        public abstract void StartFiring();
        public abstract void StopFiring();
    }
}