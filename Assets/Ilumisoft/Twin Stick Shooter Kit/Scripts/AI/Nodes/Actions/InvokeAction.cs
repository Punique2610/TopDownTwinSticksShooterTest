namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using UnityEngine.Events;

    /// <summary>
    /// Calls the given method.
    /// This node always returns 'Success'
    /// </summary>
    public class InvokeAction : ActionNode
    {
        /// <summary>
        /// Reference to the method that should be invoked
        /// </summary>
        UnityAction action;

        /// <summary>
        /// An optional label that can be set to improve debugging
        /// </summary>
        public string Name { get; set; } = string.Empty;

        public InvokeAction(string name, UnityAction action)
        {
            Name = name;
            this.action = action;
        }

        protected override StatusCode OnUpdate()
        {
            action?.Invoke();
            return StatusCode.Success;
        }

        public override string ToString() => Name;

    }
}