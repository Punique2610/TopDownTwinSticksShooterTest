using System;
using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public abstract class GameObjectEventNode<T> : GameObjectEventUnit<T>
    {
        public abstract string EventHook { get; }

        protected override string hookName => EventHook;

        protected virtual string OutputValueLabel => typeof(T).Name;

        [DoNotSerialize]
        public ValueOutput Output { get; private set; }

        protected override void Definition()
        {
            base.Definition();

            Output = ValueOutput<T>(OutputValueLabel);
        }

        protected override void AssignArguments(Flow flow, T args)
        {
            flow.SetValue(Output, args);
        }

        public override Type MessageListenerType { get; }
    }

    public abstract class GameObjectEventNode : GameObjectEventUnit<EmptyEventArgs>
    {
        public abstract string EventHook { get; }

        protected override string hookName => EventHook;

        public override Type MessageListenerType { get; }
    }
}