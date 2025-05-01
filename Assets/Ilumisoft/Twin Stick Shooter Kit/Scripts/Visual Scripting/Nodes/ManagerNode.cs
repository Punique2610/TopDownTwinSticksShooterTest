using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public abstract class ManagerNode<T> : Unit
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput OutputTrigger;

        protected override void Definition()
        {
            InputTrigger = ControlInput("InputTrigger", ExecuteFlow);
            OutputTrigger = ControlOutput("OutputTrigger");

            Succession(InputTrigger, OutputTrigger);
        }

        private ControlOutput ExecuteFlow(Flow flow)
        {
            if(Managers.TryGet<T>(out var manager))
            {
                OnExecuteFlow(flow, manager);
            }

            return OutputTrigger;
        }

        protected abstract void OnExecuteFlow(Flow flow, T manager);
    }
}