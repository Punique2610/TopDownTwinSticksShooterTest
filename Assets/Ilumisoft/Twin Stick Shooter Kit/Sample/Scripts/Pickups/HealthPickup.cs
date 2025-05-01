using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class HealthPickup : CollectableComponent
    {
        [SerializeField, Min(0)]
        int HealthAmount = 100;

        [SerializeField]
        GameObject vfxPrefab;

        /// <summary>
        /// The health pickup can only be collected by the player and only if the health is not already full
        /// </summary>
        /// <param name="collector"></param>
        /// <returns></returns>
        public override bool CanBeCollected(GameObject collector)
        {
            if (base.CanBeCollected(gameObject) && collector.CompareTag("Player") && collector.TryGetComponent<IHealthComponent>(out var healthComponent))
            {
                return healthComponent.CurrentHealth < healthComponent.MaxHealth;
            }

            return false;
        }

        protected override void OnCollect(GameObject collector)
        {
            if (collector.TryGetComponent<IHealthComponent>(out var healthComponent))
            {
                healthComponent.AddHealth(HealthAmount);

                if (vfxPrefab != null)
                {
                    Instantiate(vfxPrefab, transform.position, transform.rotation);
                }

                Destroy(gameObject);
            }
        }
    }
}