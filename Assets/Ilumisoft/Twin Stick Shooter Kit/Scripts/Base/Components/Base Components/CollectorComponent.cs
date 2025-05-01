using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the collector component
    /// </summary>
    public abstract class CollectorComponent : MonoBehaviour, ICollectorComponent
    {
        public GameObject GameObject => gameObject;

        public abstract void Collect();
    }
}