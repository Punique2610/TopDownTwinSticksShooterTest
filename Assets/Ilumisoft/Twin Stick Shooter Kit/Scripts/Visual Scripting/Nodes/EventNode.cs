using Unity.VisualScripting;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    /// <summary>
    /// Base class for all nodes with a single argument
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EventNode<T> : EventUnit<T>
    {
        [DoNotSerialize]
        public ValueOutput Output { get; private set; }

        public abstract string EventHook { get; }

        public virtual string Label => typeof(T).Name;

        protected override bool register => true;

        public override EventHook GetHook(GraphReference reference)
        {
            return new EventHook(EventHook);
        }

        protected override void Definition()
        {
            base.Definition();

            Output = ValueOutput<T>(Label);
        }

        protected override void AssignArguments(Flow flow, T data)
        {
            flow.SetValue(Output, data);
        }
    }
}