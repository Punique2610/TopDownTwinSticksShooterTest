using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class WeaponPickup : CollectableComponent
    {
        [SerializeField]
        WeaponComponent weaponPrefab;

        [SerializeField]
        GameObject vfxPrefab;

        /// <summary>
        /// The weapon pickup can only be collected by the player
        /// </summary>
        /// <param name="collector"></param>
        /// <returns></returns>
        public override bool CanBeCollected(GameObject collector)
        {
            return base.CanBeCollected(gameObject) && collector.CompareTag("Player");
        }

        protected override void OnCollect(GameObject collector)
        {
            if (collector.TryGetComponent<IWeaponControllerComponent>(out var weaponController))
            {
                weaponController.Equip(weaponPrefab);

                if (vfxPrefab != null)
                {
                    Instantiate(vfxPrefab, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
}