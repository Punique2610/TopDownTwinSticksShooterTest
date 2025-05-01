using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Collect")]
    [UnitSurtitle("Collector")]
    [UnitCategory("Twin Stick Shooter Kit\\Collector")]
    [TypeIcon(typeof(CollectorComponent))]
    public class CollectNode : ComponentNode<ICollectorComponent>
    {
        protected override void OnExecuteFlow(Flow flow, ICollectorComponent component)
        {
            component.Collect();
        }
    }
}