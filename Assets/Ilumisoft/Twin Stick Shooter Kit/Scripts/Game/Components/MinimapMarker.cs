using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MinimapMarker : MinimapMarkerComponent, IMinimapMarkerComponent
    {
        public CanvasGroup CanvasGroup { get; set; }

        private void Awake()
        {
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public override void SetVisible(bool visibility)
        {
            CanvasGroup.alpha = visibility ? 1.0f : 0.0f;
        }
    }
}