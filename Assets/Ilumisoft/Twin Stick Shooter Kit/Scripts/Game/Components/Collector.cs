using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the collector component.
    /// It allows to define a layer mask, detection radius and has an auto collect mode.
    /// </summary>
    [AddComponentMenu("Twin Stick Shooter Kit/Game/Collector")]
    public class Collector : CollectorComponent
    {
        /// <summary>
        /// Layers enabled for collision detection
        /// </summary>
        [Tooltip("Layers enabled for collision detection with collectables")]
        public LayerMask LayerMask = 0;

        /// <summary>
        /// Radius of the collision detection
        /// </summary>
        [Tooltip("Radius of the collectable detection")]
        public float Radius = 1.0f;

        /// <summary>
        /// Offset of the transform position, used as the center of the sphere overlap detection
        /// </summary>
        [Tooltip("Offset of the transform position, used as the center of the sphere overlap detection")]
        public Vector3 Offset = Vector3.zero;

        [SerializeField]
        [Tooltip("All collectables will be collected automatically when entered")]
        bool autoCollect = true;

        /// <summary>
        /// List holding all collectables which have been detected
        /// </summary>
        readonly List<GameObject> collectableObjects = new(20);

        /// <summary>
        /// Sphere Overlap collision detection used to detect collectables
        /// </summary>
        protected SphereOverlapDetection CollisionDetection { get; set; }

        private void Awake()
        {
            CollisionDetection = new SphereOverlapDetection(capacity: 20);
        }

        void Update()
        {
            // Update collision settings
            CollisionDetection.LayerMask = LayerMask;
            CollisionDetection.QueryTriggerInteraction = QueryTriggerInteraction.Collide;
            CollisionDetection.Position = transform.position;
            CollisionDetection.Radius = Radius;

            // Get colliders
            var colliders = CollisionDetection.GetColliders();

            // Remember the last amount of collectables in range
            int lastCount = collectableObjects.Count;

            collectableObjects.Clear();

            // Add all collectables in a list
            foreach (var collider in colliders)
            {
                if (TryGetCollectable(collider, out var collectable))
                {
                    collectableObjects.Add(collectable);
                }
            }

            // Get the current amount of collectables in range
            int currentCount = collectableObjects.Count;

            // If the number of collectables is larger than last frame, a collectable has been entered
            if (currentCount > lastCount)
            {
                if(autoCollect)
                {
                    Collect();
                }
            }
        }

        /// <summary>
        /// Returns true if the colliders belongs to a collectable and returns the game object containing the collectable component
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="collectable"></param>
        /// <returns></returns>
        bool TryGetCollectable(Collider collider, out GameObject collectable)
        {
            collectable = null;

            if (collider.attachedRigidbody != null)
            {
                var rigidbody = collider.attachedRigidbody;

                if (rigidbody.TryGetComponent<ICollectableComponent>(out _))
                {
                    collectable = rigidbody.gameObject;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Collects all collectables that are in range
        /// </summary>
        public override void Collect()
        {
            foreach (var element in collectableObjects)
            {
                // Skip null or inactive elements
                if (element == null || element.activeSelf == false)
                {
                    continue;
                }

                // Get the collectable component
                if (element.TryGetComponent<ICollectableComponent>(out var collectable))
                {
                    collectable.Collect(gameObject);
                }
            }

            // Clear the list of collectable objects in range
            collectableObjects.Clear();
        }

        /// <summary>
        /// Draws gizmos to visualize collision detection in the editor
        /// </summary>
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + Offset, Radius);
        }
    }
}