using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Manager Provider.
    /// This gets created and injected into the Managers class automatically at startup.
    /// </summary>
    public class DefaultManagerProvider : MonoBehaviour, IManagerProvider
    {
        readonly Dictionary<Type, object> cache = new();

        public void Register(ManagerComponent manager)
        {
            manager.transform.SetParent(transform);
        }

        public T Get<T>()
        {
            if (TryGet(out T manager))
            {
                return manager;
            }
            else
            {
                throw new ManagerNotFoundException($"Manager of type {typeof(T).Name} does not exist");
            }
        }

        public bool TryGet<T>(out T manager)
        {
            var key = typeof(T);

            // Try to get manager from cache
            if (cache.TryGetValue(key, out var result))
            {
                manager = (T)result;
                return true;
            }
            // Try to find manager in children
            else
            {
                result = GetComponentInChildren<T>();

                // If manager has been found, add it to the cache and return it
                if (result != null)
                {
                    cache.Add(key, result);

                    manager = (T)result;

                    return true;
                }
            }

            manager = default;

            return false;
        }
    }
}