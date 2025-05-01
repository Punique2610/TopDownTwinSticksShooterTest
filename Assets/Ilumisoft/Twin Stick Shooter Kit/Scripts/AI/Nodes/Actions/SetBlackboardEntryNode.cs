namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;

    /// <summary>
    /// Sets an entry to the blackboard.
    /// The value can either be 'fixed' or dynamic by using a method.
    /// This node always returns 'Success'.
    /// </summary>
    public class SetBlackboardEntryNode<T> : ActionNode
    {
        /// <summary>
        /// The key of the entry
        /// </summary>
        readonly string key;

        /// <summary>
        /// Reference to the method providing the value that should be set
        /// </summary>
        Func<T> valueFunction = null;

        /// <summary>
        /// The value that should be set
        /// </summary>
        T value;

        public SetBlackboardEntryNode(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        public SetBlackboardEntryNode(string key, Func<T> value)
        {
            this.key = key;
            this.valueFunction = value;
        }

        protected override StatusCode OnUpdate()
        {
            // If a method has been set, the entry will be set using the method
            if (valueFunction != null)
            {
                Blackboard.Set(key, valueFunction());
            }
            // Otherwise the 'fixed' value will be used
            else
            {
                Blackboard.Set(key, value);
            }

            return StatusCode.Success;
        }

        public override string ToString() => $"Set Blackboard entry: {key}";
    }
}