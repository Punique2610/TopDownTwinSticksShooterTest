using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Ilumisoft.TwinStickShooterKit
{
    public class WeaponInfo : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI AmmoText;

        public IWeaponComponent Weapon { get; protected set; } = null;

        public void BindWeapon(IWeaponComponent weapon)
        {
            if (!weapon.IsNullOrDestroyed())
            {
                UnbindCurrentWeapon();

                Weapon = weapon;

                if (weapon.TryGetComponent<IWeaponIcon>(out var weaponIcon))
                {
                    SetIcon(weaponIcon.GetIcon());
                }

                if (weapon is IUseAmmo ammoWeapon)
                {
                    SetText(ammoWeapon.Ammo.ToString());

                    ammoWeapon.OnAmmoAmountChanged += OnAmmoAmountChanged;
                }
            }
        }

        private void OnAmmoAmountChanged()
        {
            if (!Weapon.IsNullOrDestroyed() && Weapon is IUseAmmo ammoWeapon)
            {
                SetText(ammoWeapon.Ammo.ToString());
            }
        }

        void UnbindCurrentWeapon()
        {
            if (!Weapon.IsNullOrDestroyed() && Weapon is IUseAmmo ammoWeapon)
            {
                ammoWeapon.OnAmmoAmountChanged -= OnAmmoAmountChanged;
            }
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetText(string ammo)
        {
            AmmoText.text = ammo;
        }
    }
}