using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IActorManager : IComponent
    {
        List<GameObject> Actors { get; set; }
        UnityAction<GameObject> OnActorAdded { get; set; }
        UnityAction<GameObject> OnActorRemoved { get; set; }

        void Register(GameObject actor);
        void Unregister(GameObject actor);
    }
}