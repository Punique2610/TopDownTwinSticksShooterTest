using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    [DefaultExecutionOrder(-10)]
    public class Actor : MonoBehaviour
    {
        public IActorManager ActorManager { get; protected set; }

        protected virtual void Awake()
        {
            ActorManager = Managers.Get<IActorManager>();
        }

        private void OnEnable()
        {
            if (!ActorManager.IsNullOrDestroyed())
            {
                ActorManager.Register(gameObject);
            }
        }

        private void OnDisable()
        {
            if (!ActorManager.IsNullOrDestroyed())
            {
                ActorManager.Unregister(gameObject);
            }
        }
    }
}