using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    [DefaultExecutionOrder(-1)]
    public class MissionInfoHUD : MonoBehaviour
    {
        /// <summary>
        /// Parent transform of all mission info elements
        /// </summary>
        public Transform Container;

        /// <summary>
        /// Pefab used to create mission info elements
        /// </summary>
        public MissionInfoUI MissionInfoPrefab;

        /// <summary>
        /// Reference to the mission system
        /// </summary>
        IMissionManager MissionService { get; set; }

        /// <summary>
        /// Contains a mission info element for each registered mission
        /// </summary>
        readonly Dictionary<GameObject, MissionInfoUI> MissionDictionary = new();

        private void Awake()
        {
            MissionService = Managers.Get<IMissionManager>();
        }

        private void OnEnable()
        {
            // Start listening to mission added/removed events
            if (MissionService != null)
            {
                MissionService.OnMissionAdded += OnMissionAdded;
                MissionService.OnMissionRemoved += OnMissionRemoved;
            }
        }

        private void OnDisable()
        {
            // Stop listening to mission added/removed events
            if (MissionService != null)
            {
                MissionService.OnMissionAdded -= OnMissionAdded;
                MissionService.OnMissionRemoved -= OnMissionRemoved;
            }
        }

        /// <summary>
        /// Callback invoked when a mission has been added
        /// </summary>
        /// <param name="mission"></param>
        private void OnMissionAdded(GameObject mission)
        {
            // Adds a mission info for the added mission
            AddMissionInfo(mission);
        }

        /// <summary>
        /// Callback invoked when a mission has been removed
        /// </summary>
        /// <param name="mission"></param>
        private void OnMissionRemoved(GameObject mission)
        {
            // Removes the mission info belonging to the removed mission
            RemoveMissionInfo(mission);
        }

        /// <summary>
        /// Creates a new mission info for the given mission and adds it to the dictionary
        /// </summary>
        /// <param name="mission"></param>
        private void AddMissionInfo(GameObject mission)
        {   
            // Cancel if no info prefab exists
            if(MissionInfoPrefab == null)
            {
                return;
            }

            // We need to initialize the mission info before awake is executed, therefore
            // the prefab should be disabled before instantiating it. If we don't do that
            // we could not safely access the mission property in a visual graph.
            MissionInfoPrefab.gameObject.SetActive(false);

            // Instantiate the info, initialize it and enable it afterwards
            var missionInfoUI = Instantiate(MissionInfoPrefab, Container);
            missionInfoUI.Initialize(mission);
            missionInfoUI.gameObject.SetActive(mission.gameObject.activeSelf);

            MissionDictionary.Add(mission, missionInfoUI);

            // We can reenable the prefab afterwards
            MissionInfoPrefab.gameObject.SetActive(true);
        }

        /// <summary>
        /// Removes the mission info belonging to the given mission
        /// </summary>
        /// <param name="mission"></param>
        private void RemoveMissionInfo(GameObject mission)
        {
            // Remove the mission info when the mission has been removed
            if (MissionDictionary.TryGetValue(mission, out var missionInfo))
            {
                // Destroy the mission info element
                Destroy(missionInfo.gameObject);

                // Remove the entry from the dictionary
                MissionDictionary.Remove(mission);
            }
        }
    }
}