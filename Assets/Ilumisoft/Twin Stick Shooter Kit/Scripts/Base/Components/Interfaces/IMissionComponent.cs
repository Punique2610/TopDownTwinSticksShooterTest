using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMissionComponent : IComponent
    {
        UnityAction OnStateChanged { get; set; }
        MissionState State { get; }

        void CompleteMission();
        void FailMission();
    }
}