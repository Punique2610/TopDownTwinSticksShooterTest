using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMissionManager : IComponent
    {
        List<GameObject> Missions { get; set; }
        UnityAction<GameObject> OnMissionAdded { get; set; }
        UnityAction<GameObject> OnMissionRemoved { get; set; }

        void Register(GameObject mission);
        void Unregister(GameObject mission);
    }
}