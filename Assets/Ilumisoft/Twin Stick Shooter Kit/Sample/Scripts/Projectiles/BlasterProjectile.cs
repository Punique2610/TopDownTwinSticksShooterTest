using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class BlasterProjectile : MonoBehaviour
    {
        public GameObject Owner { get; set; }

        public LayerMask LayerMask = -1;

        public Collider[] OwnerColliders = new Collider[0];

        public float speed = 10.0f;
        public float damage;

        public float radius;

        public float maxLifetime = 5.0f;

        Vector3 previousPosition;

        HitDetection HitDetection { get; set; }


        Timer selfDestroyTimer;

        ISpawningManager SpawningManager { get; set; }

        [SerializeField]
        GameObject impactVFXPrefab = null;

        public void Initialize(GameObject owner)
        {
            Owner = owner;

            // Get all colliders belonging to the owner in order to avoid self collision
            OwnerColliders = owner.GetComponentsInChildren<Collider>();

            HitDetection = new HitDetection(20);

            // Use the current position as an initial value for the previous position
            previousPosition = transform.position;

            selfDestroyTimer.Restart();
        }

        private void Awake()
        {
            SpawningManager = Managers.Get<ISpawningManager>();

            selfDestroyTimer = new Timer
            {
                Duration = maxLifetime
            };
        }

        private void Update()
        {
            if (selfDestroyTimer.IsElapsed)
            {
                Despawn();

                return;
            }

            selfDestroyTimer.Tick(Time.deltaTime);

            // Move
            transform.position += speed * Time.deltaTime * transform.forward;

            // Detect and process hits
            PerformHitDetection();

            // Remember current position
            previousPosition = transform.position;
        }

        void PerformHitDetection()
        {
            // Update hit detection settings
            HitDetection.LayerMask = LayerMask;
            HitDetection.Radius = radius;

            // Detect all hits between the previous and the current position
            var raycastHits = HitDetection.DetectHit(previousPosition, transform.position);

            // If there is a valid hit, try to get the closest one
            if (TryGetClosestValidHit(raycastHits, out var hitInfo))
            {
                // Perform the hit action
                OnHit(hitInfo);
            }
        }

        bool TryGetClosestValidHit(List<RaycastHit> hits, out HitInfo hitInfo)
        {
            hitInfo = default;

            RaycastHit closestHit = new RaycastHit()
            {
                distance = Mathf.Infinity
            };

            bool foundHit = false;

            foreach (var hit in hits)
            {
                if (IsHitValid(hit.collider) && hit.distance < closestHit.distance)
                {
                    foundHit = true;
                    closestHit = hit;
                }
            }

            if (foundHit)
            {
                // Handle case of casting while already inside a collider
                if (closestHit.distance <= 0f)
                {
                    closestHit.point = transform.position;
                    closestHit.normal = -transform.forward;
                }

                hitInfo = new HitInfo()
                {
                    Position = closestHit.point,
                    Normal = closestHit.normal,
                    Collider = closestHit.collider,
                };
            }

            return foundHit;
        }

        bool IsHitValid(Collider hit)
        {
            // Ignore colliders which are triggers and do not have a hitbox
            if (hit.isTrigger && hit.GetComponent<IHitboxComponent>() == null)
            {
                return false;
            }

            if (hit.TryGetComponent<IHitboxComponent>(out var hitbox))
            {
                if (hitbox.Health.GameObject == Owner)
                {
                    return false;
                }
            }

            foreach (var collider in OwnerColliders)
            {
                if (hit == collider)
                {
                    return false;
                }
            }

            return true;
        }

        void OnHit(HitInfo hitInfo)
        {
            // Apply damage when collider is a hitbox
            if (hitInfo.Collider.TryGetComponent<IHitboxComponent>(out var hitbox))
            {
                hitbox.ApplyDamage(damage);
            }

            if (impactVFXPrefab != null)
            {
                SpawningManager.Spawn(impactVFXPrefab, hitInfo.Position, Quaternion.LookRotation(hitInfo.Normal));
            }

            // Self Destruct
            Despawn();
        }

        private void Despawn()
        {
            SpawningManager.Despawn(gameObject);
        }
    }
}