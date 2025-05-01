using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the weapon component
    /// </summary>
    public abstract class WeaponComponent : MonoBehaviour, IWeaponComponent
    {
        public GameObject Owner { get; set; }

        public bool IsTriggerDown { get; protected set; }

        public UnityAction OnPullTrigger { get; set; }
        public UnityAction OnReleaseTrigger { get; set; }
        public UnityAction OnFire { get; set; }

        /// <summary>
        /// The game object this component is attached to
        /// </summary>
        public GameObject GameObject => gameObject;

        public virtual void PullTrigger()
        {
            IsTriggerDown = true;

            OnPullTrigger?.Invoke();
        }

        public virtual void ReleaseTrigger()
        {
            IsTriggerDown = false;

            OnReleaseTrigger?.Invoke();
        }
    }
}