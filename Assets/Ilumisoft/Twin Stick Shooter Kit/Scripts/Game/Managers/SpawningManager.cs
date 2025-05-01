using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Spawning Manager
    /// </summary>
    public class SpawningManager : ManagerComponent, ISpawningManager
    {
        IPoolManager PoolManager { get; set; }

        public override void OnInitialize()
        {
            PoolManager = Managers.Get<IPoolManager>();
        }

        public GameObject Spawn(GameObject prefab, Transform parent = null)
        {
            return Spawn(prefab, Vector3.zero, Quaternion.identity, parent);
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var pool = PoolManager.GetPool(prefab);

            GameObject instance;

            // Create a new poolable instance if the pool is empty
            if (pool.IsEmpty)
            {
                prefab.SetActive(false);
                instance = Instantiate(prefab, position, rotation, parent);
                prefab.SetActive(true);

                var poolable = instance.AddComponent<Poolable>();
                poolable.Pool = pool;
            }
            // Use a pooled instance otherwise
            else
            {
                instance = pool.Get();
                instance.transform.SetPositionAndRotation(position, rotation);
                instance.transform.SetParent(parent);
            }

            // Get spawnable component or add it if none exists
            if (!instance.TryGetComponent<SpawningCallbackSender>(out var spawningCallbackSender))
            {
                spawningCallbackSender = instance.AddComponent<SpawningCallbackSender>();
            }

            instance.SetActive(true);

            spawningCallbackSender.OnSpawn();

            return instance;
        }

        public void Despawn(GameObject gameObject)
        {
            // Invoke the OnDespawn event if the game object has a spawnable component
            if (!gameObject.TryGetComponent<SpawningCallbackSender>(out var spawningCallbackSender))
            {
                spawningCallbackSender = gameObject.AddComponent<SpawningCallbackSender>();
            }

            spawningCallbackSender.OnDespawn();

            // If the object is poolable, disable it and return it to the corresponding object pool
            if (gameObject.TryGetComponent<Poolable>(out var poolable) && poolable.Pool != null)
            {
                gameObject.SetActive(false);
                poolable.Pool.Add(gameObject);
            }
            // Otherwise the object will be destroyed
            else
            {
                Destroy(gameObject);
            }
        }
    }
}