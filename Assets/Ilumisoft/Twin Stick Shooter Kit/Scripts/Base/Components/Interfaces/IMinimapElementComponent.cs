using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMinimapElementComponent : IComponent
    {
        bool AlwaysVisible { get; set; }
        IMinimapMarkerComponent CreateMarker();
    }
}