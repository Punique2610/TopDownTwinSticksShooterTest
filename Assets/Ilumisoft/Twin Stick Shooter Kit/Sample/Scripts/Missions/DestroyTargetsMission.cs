using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Simple sample mission, which allows to assign a list of targets, which need to be destroyed.
    /// </summary>
    [AddComponentMenu("Mission/Destroy Targets Mission", order: 1)]
    public class DestroyTargetsMission : MissionComponent
    {
        /// <summary>
        /// List of all targets that need to be destroyed/killed
        /// </summary>
        public List<GameObject> targets = new();

        /// <summary>
        /// Counter for the amount of targets that have been destroyed/killed
        /// </summary>
        int destroyedTargetsCount = 0;

        IMissionInfoComponent MissionInfo { get; set; }

        protected override void Awake()
        {
            base.Awake();

            MissionInfo = GetComponent<IMissionInfoComponent>();
        }

        /// <summary>
        /// Start listening to health empty events of the targets when enabled
        /// </summary>
        protected override void OnEnable()
        {
            base.OnEnable();

            foreach (var target in targets)
            {
                if (target != null && target.TryGetComponent<IHealthComponent>(out var health))
                {
                    health.OnHealthEmpty += OnTargetDestroyed;
                }
            }

            // Set intial info text
            UpdateInfoText();
        }

        /// <summary>
        /// Stop listening to health empty events of the targets when disabled
        /// </summary>
        protected override void OnDisable()
        {
            base.OnDisable();

            foreach (var target in targets)
            {
                if (target != null && target.TryGetComponent<IHealthComponent>(out var health))
                {
                    health.OnHealthEmpty -= OnTargetDestroyed;
                }
            }
        }

        /// <summary>
        /// Increases the target destroyed counter and updates the info text when a target gets destroyed
        /// </summary>
        private void OnTargetDestroyed()
        {
            destroyedTargetsCount++;
            UpdateInfoText();
        }

        private void UpdateInfoText()
        {
            MissionInfo.SetInfoText($"{destroyedTargetsCount}/{targets.Count}");
        }

        private void Update()
        {
            // Cancel if the mission is not active
            if (State != MissionState.Active)
            {
                return;
            }

            // Complete the mission when all targets has been destroyed
            if (destroyedTargetsCount >= targets.Count)
            {
                CompleteMission();
            }
        }
    }
}