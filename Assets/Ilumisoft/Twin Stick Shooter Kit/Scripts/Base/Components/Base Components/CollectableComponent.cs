using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public abstract class CollectableComponent : MonoBehaviour, ICollectableComponent
    {
        public GameObject GameObject => gameObject;
        protected bool hasBeenCollected = false;

        public virtual bool CanBeCollected(GameObject collector) => !hasBeenCollected;

        protected abstract void OnCollect(GameObject collector);

        public virtual void Collect(GameObject collector)
        {
            if (CanBeCollected(collector))
            {
                // Remember that the collectable has been collected
                hasBeenCollected = true;

                // Invoke callback method
                OnCollect(collector);

                // Broadcast message
                Messages.Publish(new ObjectCollectedMessage
                {
                    Collectable = gameObject,
                    Collector = collector
                });
            }
        }

        protected virtual void Reset()
        {
            // Add a rigidbody to the game object if none exists when the component is added or reset.
            // By default the rigibody is set to kinematic, since we do not expect collectables to
            // be driven by physics.
            if (TryGetComponent<Rigidbody>(out _) == false)
            {
                var rigidbody = gameObject.AddComponent<Rigidbody>();

                rigidbody.isKinematic = true;
            }
        }
    }
}