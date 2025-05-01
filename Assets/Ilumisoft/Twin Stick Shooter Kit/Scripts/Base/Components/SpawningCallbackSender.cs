using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public class SpawningCallbackSender : MonoBehaviour
    {
        protected ISpawnCallbackReceiver[] SpawnCallbackReceivers { get; set; }
        protected IDespawnCallbackReceiver[] DespawnCallbackReceivers { get; set; }

        protected virtual void Awake()
        {
            SpawnCallbackReceivers = GetComponents<ISpawnCallbackReceiver>();
            DespawnCallbackReceivers = GetComponents<IDespawnCallbackReceiver>();
        }

        public void OnSpawn()
        {
            foreach (var receiver in SpawnCallbackReceivers)
            {
                if (receiver != null)
                {
                    receiver.OnSpawn();
                }
            }

            Messages.Publish(new ObjectSpawnedMessage
            {
                Sender = gameObject
            });
        }

        public void OnDespawn()
        {
            foreach (var receiver in DespawnCallbackReceivers)
            {
                if (receiver != null)
                {
                    receiver.OnDespawn();
                }
            }

            Messages.Publish(new ObjectDespawnedMessage
            {
                Sender = gameObject
            });
        }
    }
}