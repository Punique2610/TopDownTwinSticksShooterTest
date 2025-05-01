using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public class MissionInfo : MonoBehaviour, IMissionInfoComponent
    {
        [SerializeField]
        private string title = "Title";

        [SerializeField]
        private string description = "Description";

        /// <summary>
        /// Gets or sets the title of the mission
        /// </summary>
        public string Title { get => title; set => title = value; }

        /// <summary>
        /// Gets or sets the description of the mission
        /// </summary>
        public string Description { get => description; set => description = value; }

        /// <summary>
        /// Gets the info text of the message
        /// </summary>
        public string InfoText { get; protected set; }

        /// <summary>
        /// Callback invoked when the info text has changed
        /// </summary>
        public UnityAction OnInfoChanged { get; set; }

        /// <summary>
        /// Reference to the game object owning this component
        /// </summary>
        public GameObject GameObject => gameObject;

        /// <summary>
        /// Updates the info text of the mission and invokes the info changed callback
        /// </summary>
        /// <param name="infoText"></param>
        /// <returns></returns>
        public void SetInfoText(string infoText)
        {
            if (InfoText != infoText)
            {
                InfoText = infoText;

                OnInfoChanged?.Invoke();
            }
        }
    }
}