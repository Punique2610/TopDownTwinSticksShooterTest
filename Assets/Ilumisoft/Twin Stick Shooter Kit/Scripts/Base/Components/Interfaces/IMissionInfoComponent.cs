using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMissionInfoComponent : IComponent
    {
        string Description { get; set; }
        string InfoText { get; }
        UnityAction OnInfoChanged { get; set; }
        string Title { get; set; }

        void SetInfoText(string infoText);
    }
}