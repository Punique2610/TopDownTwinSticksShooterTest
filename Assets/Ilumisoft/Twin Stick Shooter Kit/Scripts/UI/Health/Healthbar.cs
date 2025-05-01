using UnityEngine;
using UnityEngine.UI;

namespace Ilumisoft.TwinStickShooterKit.UI
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField]
        Image fillImage;

        [SerializeField, Tooltip("Whether the healthbar should be hidden when health is empty")]
        bool hideEmpty = false;

        [SerializeField, Tooltip("Makes the healthbar being aligned with the camera")]
        bool alignWithCamera = false;

        [SerializeField, Tooltip("Controls how fast changes will be animated")]
        float smoothingValue = 10;

        float currentValue;

        public IHealthComponent Health { get; set; }

        protected virtual void Awake()
        {
            Health = GetComponentInParent<IHealthComponent>();
        }

        private void OnEnable()
        {
            if (!Health.IsNullOrDestroyed())
            {
                Health.OnHealthEmpty += OnHealthEmpty;
            }
        }

        private void OnDisable()
        {
            if (!Health.IsNullOrDestroyed())
            {
                Health.OnHealthEmpty -= OnHealthEmpty;
            }
        }

        private void Start()
        {
            currentValue = Health.CurrentHealth;
        }

        private void Update()
        {
            if (alignWithCamera)
            {
                AlignWithCamera();
            }

            currentValue = Mathf.Lerp(currentValue, Health.CurrentHealth, Time.deltaTime * smoothingValue);
            UpdateFillbar();
        }

        private void AlignWithCamera()
        {
            transform.forward = Camera.main.transform.forward;
        }

        void UpdateFillbar()
        {
            float value = Mathf.InverseLerp(0, Health.MaxHealth, currentValue);

            fillImage.fillAmount = value;
        }

        private void OnHealthEmpty()
        {
            if (hideEmpty)
            {
                GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            }
        }
    }
}