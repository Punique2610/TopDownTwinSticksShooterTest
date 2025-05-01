using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the IPool interface. 
    /// This class is used by the default Pool Manager to provide pools for object pooling.
    /// </summary>
    [AddComponentMenu("")]
    public class Pool : MonoBehaviour, IPool
    {
        readonly Stack<GameObject> elements = new();

        public bool IsEmpty => elements.Count == 0;

        public GameObject Get()
        {
            return elements.Pop();
        }

        public void Add(GameObject gameObject)
        {
            elements.Push(gameObject);
            gameObject.transform.SetParent(transform);
        }
    }
}