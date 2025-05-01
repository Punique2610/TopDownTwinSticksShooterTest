using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Trigger Game Over")]
    [UnitCategory("Twin Stick Shooter Kit\\Game")]
    public class TriggerGameOverNode : Unit
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput InputTrigger { get; private set; }

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput OutputTrigger { get; private set; }

        [DoNotSerialize]
        private ValueInput winValue;

        protected override void Definition()
        {
            InputTrigger = ControlInput(nameof(InputTrigger), Trigger);
            OutputTrigger = ControlOutput(nameof(OutputTrigger));

            winValue = ValueInput<bool>(key: nameof(winValue), @default: true);

            Succession(InputTrigger, OutputTrigger);
        }

        private ControlOutput Trigger(Flow flow)
        {
            bool hasWon = flow.GetValue<bool>(winValue);

            Messages.Publish(new GameOverMessage()
            {
                HasWon = hasWon
            });

            return OutputTrigger;
        }
    }
}