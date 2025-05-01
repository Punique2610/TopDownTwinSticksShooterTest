using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for minimap element component
    /// </summary>
    public abstract class MinimapElementComponent : MonoBehaviour, IMinimapElementComponent
    {
        public GameObject GameObject => gameObject;
        public abstract bool AlwaysVisible { get; set; }
        public abstract IMinimapMarkerComponent CreateMarker();
    }
}