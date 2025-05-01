using TMPro;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// UI Element containing information about a specific mission, for example
    /// the title and description of the mission
    /// </summary>
    public class MissionInfoUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI titleText;

        [SerializeField]
        private TextMeshProUGUI descriptionText;

        [SerializeField]
        private TextMeshProUGUI infoText;

        /// <summary>
        /// Reference to the owner of the mission
        /// </summary>
        public GameObject Mission { get; private set; }

        /// <summary>
        /// Reference to the mission the mission info belongs to
        /// </summary>
        public IMissionInfoComponent MissionInfo { get; private set; }

        /// <summary>
        /// Initializes the mission info with the given mission
        /// </summary>
        /// <param name="mission"></param>
        public void Initialize(GameObject missionOwner)
        {
            this.Mission = missionOwner;

            if (Mission != null && Mission.TryGetComponent<IMissionInfoComponent>(out var missionInfo))
            {
                MissionInfo = missionInfo;
                titleText.text = MissionInfo.Title;
                infoText.text = MissionInfo.InfoText;
                descriptionText.text = MissionInfo.Description;
            }
        }

        /// <summary>
        /// Start listening to info change events
        /// </summary>
        private void OnEnable()
        {
            if (!MissionInfo.IsNullOrDestroyed())
            {
                MissionInfo.OnInfoChanged += OnInfoChanged;
            }
        }

        /// <summary>
        /// Stop listening to info change events
        /// </summary>
        private void OnDisable()
        {
            if (!MissionInfo.IsNullOrDestroyed())
            {
                MissionInfo.OnInfoChanged -= OnInfoChanged;
            }
        }

        /// <summary>
        /// Callback invoked when the mission info has changed
        /// </summary>
        protected virtual void OnInfoChanged()
        {
            infoText.text = MissionInfo.InfoText;
        }

        /// <summary>
        /// Sets the color of all texts to the given value
        /// </summary>
        /// <param name="color"></param>
        public void SetTextColor(Color color)
        {
            titleText.color = color;
            descriptionText.color = color;
            infoText.color = color;
        }
    }
}