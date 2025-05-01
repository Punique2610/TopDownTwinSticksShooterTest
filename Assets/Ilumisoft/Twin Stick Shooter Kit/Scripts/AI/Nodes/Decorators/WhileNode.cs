namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;

    /// <summary>
    /// Ticks the child node until the condition fails.
    /// When the condition is not met, this node returns 'Failure'.
    /// Otherwise it will return 'Running'.
    /// </summary>
    public class WhileNode : DecoratorNode
    {
        /// <summary>
        /// Reference to the method determining whether the condition is met or not
        /// </summary>
        readonly Func<bool> isConditionMet;

        public WhileNode(Func<bool> isConditionMet)
        {
            this.isConditionMet = isConditionMet;
        }

        protected override StatusCode OnUpdate()
        {
            if (isConditionMet != null && isConditionMet())
            {
                Child.Tick();
                return StatusCode.Running;
            }
            else
            {
                ExecuteAbortPolicy();
                return StatusCode.Failure;
            }
        }

        public override string ToString() => $"While: {isConditionMet.Method}";
    }
}