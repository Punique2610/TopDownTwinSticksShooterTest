using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for minimap marker component
    /// </summary>
    public abstract class MinimapMarkerComponent : MonoBehaviour, IMinimapMarkerComponent
    {
        public GameObject GameObject => gameObject;

        public abstract void SetVisible(bool visibility);
    }
}