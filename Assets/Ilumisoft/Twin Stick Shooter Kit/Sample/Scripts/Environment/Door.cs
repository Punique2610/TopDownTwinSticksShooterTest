using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    /// <summary>
    /// Simple sample door behaviour. It will open the door if any actor is in a given range and close it otherwise
    /// </summary>
    [RequireComponent(typeof(PlayableDirector))]
    public class Door : MonoBehaviour
    {
        /// <summary>
        /// Timelines played to open/close the door
        /// </summary>
        [SerializeField]
        PlayableAsset openTimeline, closeTimeline;

        /// <summary>
        /// The radius of the doors open/close trigger
        /// </summary>
        [SerializeField]
        float doorRadius = 3;

        /// <summary>
        /// Reference to the playable director attached to the door component
        /// </summary>
        PlayableDirector PlayableDirector { get; set; }

        NavMeshObstacle NavMeshObstacle { get; set; }

        /// <summary>
        /// Reference to the actor manager
        /// </summary>
        IActorManager ActorManager { get; set; }

        /// <summary>
        /// Whether the door is currently open or not
        /// </summary>
        bool isOpen = false;

        private void Awake()
        {
            ActorManager = Managers.Get<IActorManager>();
            PlayableDirector = GetComponent<PlayableDirector>();
            NavMeshObstacle = GetComponent<NavMeshObstacle>();
        }

        private void Start()
        {
            NavMeshObstacle.enabled = true;
        }

        private void Update()
        {
            bool isActorInDistance = false;

            // Compute if any actor is in distance
            foreach (var actor in ActorManager.Actors)
            {
                float distance = Vector3.Distance(transform.position, actor.transform.position);

                if (distance < doorRadius)
                {
                    isActorInDistance = true;
                }
            }

            // Open closed door if any actor is in distance
            if (!isOpen && isActorInDistance)
            {
                Open();
            }
            // Close opened door if no actor is in distance
            else if (isOpen && !isActorInDistance)
            {
                Close();
            }
        }

        /// <summary>
        /// Opens the door
        /// </summary>
        void Open()
        {
            isOpen = true;
            NavMeshObstacle.enabled = false;
            PlayableDirector.Play(openTimeline);
        }

        // Closes the door
        void Close()
        {
            isOpen = false;
            NavMeshObstacle.enabled = true;
            PlayableDirector.Play(closeTimeline);
        }
    }
}