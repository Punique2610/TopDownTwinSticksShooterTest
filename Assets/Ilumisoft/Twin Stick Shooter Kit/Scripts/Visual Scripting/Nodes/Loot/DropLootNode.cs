using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Drop Loot")]
    [UnitSurtitle("Loot")]
    [UnitShortTitle("Drop")]
    [UnitCategory("Twin Stick Shooter Kit\\Loot")]
    [TypeIcon(typeof(LootComponent))]
    public class DropLootNode : ComponentNode<ILootComponent>
    {
        protected override void OnExecuteFlow(Flow flow, ILootComponent loot)
        {
            loot.Drop();
        }
    }
}