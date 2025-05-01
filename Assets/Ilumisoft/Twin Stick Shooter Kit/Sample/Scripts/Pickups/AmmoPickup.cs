using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class AmmoPickup : CollectableComponent
    {
        [SerializeField]
        int AmmoAmount = 100;

        [SerializeField]
        GameObject vfxPrefab;

        /// <summary>
        /// The ammo pickup can only be collected by the player and only if the ammo is not already full
        /// </summary>
        /// <param name="collector"></param>
        /// <returns></returns>
        public override bool CanBeCollected(GameObject collector)
        {
            if (base.CanBeCollected(collector) && collector.CompareTag("Player") && collector.TryGetComponent<IWeaponControllerComponent>(out var weaponController))
            {
                var currentWeapon = weaponController.GetCurrentWeapon();

                if (currentWeapon != null && currentWeapon is IUseAmmo ammoWeapon)
                {
                    return ammoWeapon.Ammo < ammoWeapon.MaxAmmo;
                }
            }

            return false;
        }

        protected override void OnCollect(GameObject collector)
        {
            if (collector.TryGetComponent<IWeaponControllerComponent>(out var weaponController))
            {
                var currentWeapon = weaponController.GetCurrentWeapon();

                if (!currentWeapon.IsNullOrDestroyed() && currentWeapon is IUseAmmo ammoWeapon)
                {
                    ammoWeapon.AddAmmo(AmmoAmount);

                    if (vfxPrefab != null)
                    {
                        Instantiate(vfxPrefab, transform.position, transform.rotation);
                    }

                    Destroy(gameObject);
                }
            }
        }
    }
}