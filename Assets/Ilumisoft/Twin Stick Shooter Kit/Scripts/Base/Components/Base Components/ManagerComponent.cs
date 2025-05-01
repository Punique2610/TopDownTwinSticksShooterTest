using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public abstract class ManagerComponent : MonoBehaviour, IComponent
    {
        public GameObject GameObject => gameObject;

        /// <summary>
        /// Callback invoked when all managers have been created.
        /// This can be used to gather references to other managers.
        /// </summary>
        public virtual void OnInitialize() { }
    }
}