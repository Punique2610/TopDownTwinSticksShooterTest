using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitTitle("Disable Hitboxes")]
    [UnitCategory("Twin Stick Shooter Kit\\Hitbox")]
    [TypeIcon(typeof(HitboxComponent))]
    public class DisableHitboxesNode : Unit
    {
        [DoNotSerialize, PortLabelHidden]
        public ControlInput InputTrigger;

        [DoNotSerialize, PortLabelHidden]
        public ControlOutput OutputTrigger;

        [DoNotSerialize, NullMeansSelf, PortLabelHidden]
        public ValueInput InputValue;

        GameObject target;

        protected override void Definition()
        {
            InputTrigger = ControlInput("InputTrigger", ExecuteFlow);
            OutputTrigger = ControlOutput("OutputTrigger");

            InputValue = ValueInput<GameObject>("InputValue", null).NullMeansSelf();

            Succession(InputTrigger, OutputTrigger);
        }

        private ControlOutput ExecuteFlow(Flow flow)
        {
            target = flow.GetValue<GameObject>(InputValue);

            var hitboxes = target.GetComponentsInChildren<IHitboxComponent>();

            foreach (var hitbox in hitboxes)
            {
                if (hitbox is MonoBehaviour monoBehaviour)
                {
                    monoBehaviour.enabled = true;
                }
            }

            return OutputTrigger;
        }
    }
}