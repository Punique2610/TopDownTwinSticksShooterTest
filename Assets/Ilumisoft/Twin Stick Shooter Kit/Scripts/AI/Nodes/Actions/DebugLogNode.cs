namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using UnityEngine;

    /// <summary>
    /// Logs a debug message.
    /// This node always returns 'Success'
    /// </summary>
    public class DebugLogNode : ActionNode
    {
        readonly string message;

        public DebugLogNode(string message)
        {
            this.message = message;
        }

        protected override StatusCode OnUpdate()
        {
            Debug.Log(message);

            return StatusCode.Success;
        }

        public override string ToString()
        {
            return $"Log Message: {message}";
        }
    }
}