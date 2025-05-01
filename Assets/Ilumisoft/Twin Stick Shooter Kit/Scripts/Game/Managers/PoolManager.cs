using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Pool Manager
    /// </summary>
    public class PoolManager : ManagerComponent, IPoolManager
    {
        [SerializeField]
        [Tooltip("Determines whether the pool container should be hidden in the hierarchy")]
        bool hidePoolContainer = true;

        /// <summary>
        /// Dictionary holding all available pools
        /// </summary>
        readonly Dictionary<GameObject, Pool> poolDictionary = new();

        GameObject poolContainer = null;

        Transform PoolContainer
        {
            get
            {
                // Create a new pool container if the current reference is null.
                // This can either be the case if no container has been created yet or a new scene has been loaded and the referenced one has been destroyed.
                if (poolContainer == null)
                {
                    poolDictionary.Clear();

                    poolContainer = new GameObject("Pool Container");

                    UpdatePoolContainerHideFlags();
                }

                return poolContainer.transform;
            }
        }

        /// <summary>
        /// Gets the pool for the given prefab. If none exists, a new one will be created and returned
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public IPool GetPool(GameObject prefab)
        {
            // If a pool exists for the given prefab ( and the pool game object has not been destroyed) return it
            if (poolDictionary.TryGetValue(prefab, out Pool pool) && pool != null)
            {
                return pool;
            }
            // Otherwise create a new pool and add it to the dictionary
            else
            {
                pool = new GameObject($"{prefab.name} Pool").AddComponent<Pool>();

                pool.transform.SetParent(PoolContainer);

                poolDictionary[prefab] = pool;

                return pool;
            }
        }

        void UpdatePoolContainerHideFlags()
        {
            if (poolContainer != null)
            {
                if (hidePoolContainer)
                {
                    poolContainer.hideFlags = HideFlags.HideInHierarchy;
                }
                else
                {
                    poolContainer.hideFlags = HideFlags.None;
                }
            }
        }

        private void OnValidate()
        {
            UpdatePoolContainerHideFlags();
        }
    }
}