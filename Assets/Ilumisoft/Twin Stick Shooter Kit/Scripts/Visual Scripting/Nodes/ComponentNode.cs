using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public abstract class ComponentNode<T> : Unit where T : IComponent
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput OutputTrigger;

        [DoNotSerialize, NullMeansSelf, PortLabelHidden]
        public ValueInput TargetGameObjectInputValue;

        protected override void Definition()
        {
            InputTrigger = ControlInput("InputTrigger", ExecuteFlow);
            OutputTrigger = ControlOutput("OutputTrigger");

            TargetGameObjectInputValue = ValueInput<GameObject>(nameof(TargetGameObjectInputValue), null).NullMeansSelf();

            Succession(InputTrigger, OutputTrigger);
        }

        private ControlOutput ExecuteFlow(Flow flow)
        {
            var target = flow.GetValue<GameObject>(TargetGameObjectInputValue);

            if (target != null && target.TryGetComponent<T>(out var component))
            {
                OnExecuteFlow(flow, component);
            }

            return OutputTrigger;
        }

        protected abstract void OnExecuteFlow(Flow flow, T component);
    }
}