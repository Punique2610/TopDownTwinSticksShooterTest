using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Fail Mission")]
    [UnitSurtitle("Mission")]
    [UnitShortTitle("Fail")]
    [UnitCategory("Twin Stick Shooter Kit\\Mission")]
    [TypeIcon(typeof(MissionComponent))]
    public class FailMissionNode : ComponentNode<IMissionComponent>
    {
        protected override void OnExecuteFlow(Flow flow, IMissionComponent mission)
        {
            mission.FailMission();
        }
    }
}