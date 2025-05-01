using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Complete Mission")]
    [UnitSurtitle("Mission")]
    [UnitShortTitle("Complete")]
    [UnitCategory("Twin Stick Shooter Kit\\Mission")]
    [TypeIcon(typeof(MissionComponent))]
    public class CompleteMissionNode : ComponentNode<IMissionComponent>
    {
        protected override void OnExecuteFlow(Flow flow, IMissionComponent mission)
        {
            mission.CompleteMission();
        }
    }
}