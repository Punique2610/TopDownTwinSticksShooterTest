using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Add Health")]
    [UnitSurtitle("Health")]
    [UnitCategory("Twin Stick Shooter Kit\\Health")]
    [TypeIcon(typeof(HealthComponent))]
    public class AddHealthNode : ComponentNode<IHealthComponent>
    {
        [DoNotSerialize]
        public ValueInput HealthAmountInputValue;

        protected override void Definition()
        {
            base.Definition();
            HealthAmountInputValue = ValueInput("Amount", 0.0f);
        }

        protected override void OnExecuteFlow(Flow flow, IHealthComponent health)
        {
            float amount = flow.GetValue<float>(HealthAmountInputValue);

            health.AddHealth(amount);
        }
    }
}