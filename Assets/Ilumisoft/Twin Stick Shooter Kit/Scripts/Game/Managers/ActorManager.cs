using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Actor Manager
    /// </summary>
    [DefaultExecutionOrder(-20)]
    public class ActorManager : ManagerComponent, IActorManager
    {
        public List<GameObject> Actors { get; set; } = new List<GameObject>();

        public UnityAction<GameObject> OnActorAdded { get; set; }

        public UnityAction<GameObject> OnActorRemoved { get; set; }

        public void Register(GameObject actor)
        {
            if (!Actors.Contains(actor))
            {
                Actors.Add(actor);

                OnActorAdded?.Invoke(actor);
            }
        }

        public void Unregister(GameObject actor)
        {
            if (Actors.Contains(actor))
            {
                Actors.Remove(actor);

                OnActorRemoved?.Invoke(actor);
            }
        }
    }
}