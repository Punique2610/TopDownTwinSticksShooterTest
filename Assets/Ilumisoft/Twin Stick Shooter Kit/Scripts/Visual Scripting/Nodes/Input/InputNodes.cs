using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public abstract class InputProviderNode<TProvider, TValue> : Unit
    {
        [DoNotSerialize, NullMeansSelf, PortLabelHidden]
        public ValueInput TargetGameObjectInputValue;

        [DoNotSerialize]
        public ValueOutput Result;

        protected override void Definition()
        {
            TargetGameObjectInputValue = ValueInput<GameObject>(nameof(TargetGameObjectInputValue), null).NullMeansSelf();
            Result = ValueOutput(nameof(Result), GetValue);
        }

        TValue GetValue(Flow flow)
        {
            var target = flow.GetValue<GameObject>(TargetGameObjectInputValue);

            if (target != null && target.TryGetComponent<TProvider>(out var provider))
            {
                return GetValue(flow, provider);
            }

            return default;
        }

        protected abstract TValue GetValue(Flow flow, TProvider provider);
    }


    [UnitTitle("Get Move Input")]
    [UnitSurtitle("Move Input Provider")]
    [UnitCategory("Twin Stick Shooter Kit\\Input")]
    [TypeIcon(typeof(InputReceiverComponent))]
    public class MoveInputNode : InputProviderNode<IMoveInputProvider, Vector2>
    {
        protected override Vector2 GetValue(Flow flow, IMoveInputProvider provider)
        {
            return provider.MoveInput;
        }
    }


    [UnitTitle("Get Look Input")]
    [UnitSurtitle("Look Input Provider")]
    [UnitCategory("Twin Stick Shooter Kit\\Input")]
    [TypeIcon(typeof(InputReceiverComponent))]
    public class LookInputNode : InputProviderNode<ILookInputProvider, Vector2>
    {
        protected override Vector2 GetValue(Flow flow, ILookInputProvider provider)
        {
            return provider.LookInput;
        }
    }

    [UnitTitle("Get Sprint Input")]
    [UnitSurtitle("Sprint Input Provider")]
    [UnitCategory("Twin Stick Shooter Kit\\Input")]
    [TypeIcon(typeof(InputReceiverComponent))]
    public class SprintInputNode : InputProviderNode<ISprintInputProvider, bool>
    {
        protected override bool GetValue(Flow flow, ISprintInputProvider provider)
        {
            return provider.SprintInput;
        }
    }
}