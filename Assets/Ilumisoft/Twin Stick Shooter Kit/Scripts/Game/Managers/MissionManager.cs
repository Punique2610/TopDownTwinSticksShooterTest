using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Mission Manager
    /// </summary>
    [DefaultExecutionOrder(-20)]
    public class MissionManager : ManagerComponent, IMissionManager
    {
        public List<GameObject> Missions { get; set; } = new();

        public UnityAction<GameObject> OnMissionAdded { get; set; }

        public UnityAction<GameObject> OnMissionRemoved { get; set; }

        public void Register(GameObject mission)
        {
            if (!Missions.Contains(mission))
            {
                Missions.Add(mission);

                OnMissionAdded?.Invoke(mission);
            }
        }

        public void Unregister(GameObject mission)
        {
            if (Missions.Contains(mission))
            {
                Missions.Remove(mission);

                OnMissionRemoved?.Invoke(mission);
            }
        }
    }
}