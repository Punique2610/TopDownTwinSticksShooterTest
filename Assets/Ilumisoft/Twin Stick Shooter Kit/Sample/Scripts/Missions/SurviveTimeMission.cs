using System;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Simple sample mission, which requires the player to survive for a given duration (in seconds).
    /// </summary>
    [AddComponentMenu("Mission/Survive Time Mission", order: 3)]
    public class SurviveTimeMission : MissionComponent
    {
        // The duration the player needs to survive in order to complete the mission
        public float duration = 5.0f;

        /// <summary>
        /// Reference to the player game object
        /// </summary>
        GameObject Player { get; set; }

        /// <summary>
        /// Reference to the health component of the player
        /// </summary>
        IHealthComponent PlayerHealth { get; set; }

        /// <summary>
        /// Time elapsed since the mission has started
        /// </summary>
        float timeElapsed = 0.0f;

        IMissionInfoComponent MissionInfo { get; set; }

        protected override void Awake()
        {
            base.Awake();

            MissionInfo = GetComponent<IMissionInfoComponent>();
        }

        private void Start()
        {
            State = MissionState.Active;

            // Get the references
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerHealth = Player.GetComponent<IHealthComponent>();
        }

        private void Update()
        {
            // Cancel if the mission is not active
            if (State != MissionState.Active)
            {
                return;
            }

            // Update time
            timeElapsed += Time.deltaTime;

            // Update the info text with the time left to complete the mission
            UpdateInfoText();

            // Check for failure
            if (!PlayerHealth.IsNullOrDestroyed())
            {
                if (PlayerHealth.CurrentHealth <= 0)
                {
                    FailMission();
                }
            }

            // Check for success
            if (timeElapsed >= duration)
            {
                CompleteMission();
            }
        }

        /// <summary>
        /// Computes a nicely readable time string for the time left and sets it as the info text
        /// </summary>
        void UpdateInfoText()
        {
            float timeLeft = Mathf.Max(0, duration - timeElapsed);

            TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);

            MissionInfo.SetInfoText(timeSpan.ToString("mm':'ss"));
        }
    }
}