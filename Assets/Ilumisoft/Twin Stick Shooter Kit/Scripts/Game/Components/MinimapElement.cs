using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public class MinimapElement : MinimapElementComponent
    {
        [SerializeField]
        MinimapMarkerComponent minimapMarkerPrefab;

        [SerializeField, Tooltip("Determines whether the element will be hidden when being out of the minimap radius or will always be visible on the minimap")]
        private bool alwaysVisible = false;

        public override bool AlwaysVisible { get => alwaysVisible; set => alwaysVisible = value; }

        IMinimapManager MinimapManager { get; set; }

        private void Awake()
        {
            MinimapManager = Managers.Get<IMinimapManager>();
        }

        private void OnEnable()
        {
            if (!MinimapManager.IsNullOrDestroyed())
            {
                MinimapManager.Register(gameObject);
            }
        }

        private void OnDisable()
        {
            if (!MinimapManager.IsNullOrDestroyed())
            {
                MinimapManager.Unregister(gameObject);
            }
        }

        public override IMinimapMarkerComponent CreateMarker()
        {
            return Instantiate(minimapMarkerPrefab);
        }
    }
}