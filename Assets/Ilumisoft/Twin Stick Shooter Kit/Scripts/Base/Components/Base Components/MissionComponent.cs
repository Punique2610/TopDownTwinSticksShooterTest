using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for all missions. A mission automatically registers itself when enabled and provides callbacks
    /// for state and info changes.
    /// </summary>
    [RequireComponent(typeof(MissionInfo))]
    [AddComponentMenu("Mission/Mission", order: 0)]
    public abstract class MissionComponent : MonoBehaviour, IMissionComponent
    {
        /// <summary>
        /// Gets the current state of the mission (Active, Completed or Failed)
        /// </summary>
        public MissionState State { get; protected set; } = MissionState.Active;

        /// <summary>
        /// Reference to the mission system
        /// </summary>
        protected IMissionManager MissionManager { get; set; }

        /// <summary>
        /// Callback invoked when the mission state has changed
        /// </summary>
        public UnityAction OnStateChanged { get; set; }

        /// <summary>
        /// Reference to the game object owning the component
        /// </summary>
        public GameObject GameObject => gameObject;

        /// <summary>
        /// Gets the reference to the mission system
        /// </summary>
        protected virtual void Awake()
        {
            MissionManager = Managers.Get<IMissionManager>();
        }

        /// <summary>
        /// Registers the mission to the mission system when the component is enabled
        /// </summary>
        protected virtual void OnEnable()
        {
            Register();
        }

        /// <summary>
        /// Unregisters the mission from the mission system when the component is disabled
        /// </summary>
        protected virtual void OnDisable()
        {
            Unregister();
        }

        /// <summary>
        /// Registers the mission to the mission system
        /// </summary>
        protected virtual void Register()
        {
            if (!MissionManager.IsNullOrDestroyed())
            {
                MissionManager.Register(gameObject);
            }
        }

        /// <summary>
        /// Unregisters the mission from the mission system
        /// </summary>
        protected virtual void Unregister()
        {
            if (!MissionManager.IsNullOrDestroyed())
            {
                MissionManager.Unregister(gameObject);
            }
        }

        /// <summary>
        /// Marks the mission as completed
        /// </summary>
        public virtual void CompleteMission()
        {
            if (State == MissionState.Active)
            {
                State = MissionState.Completed;

                OnStateChanged?.Invoke();

                Messages.Publish(new MissionStateChangedMessage
                {
                    Sender = gameObject,
                    MissionState = State
                });
            }
        }

        /// <summary>
        /// Marks the mission as failed
        /// </summary>
        public virtual void FailMission()
        {
            if (State == MissionState.Active)
            {
                State = MissionState.Failed;

                OnStateChanged?.Invoke();

                Messages.Publish(new MissionStateChangedMessage
                {
                    Sender = gameObject,
                    MissionState = State
                });
            }
        }
    }
}