namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;

    /// <summary>
    /// Ticks the child node if the condition is met.
    /// Otherwise this node returns 'Failure'
    /// </summary>
    public class IfNode : DecoratorNode
    {
        /// <summary>
        /// Reference to the method determining whether the condition is met or not
        /// </summary>
        readonly Func<bool> isConditionMet;

        public IfNode(Func<bool> isConditionMet)
        {
            this.isConditionMet = isConditionMet;
        }

        protected override StatusCode OnUpdate()
        {
            if (isConditionMet != null && isConditionMet())
            {
                return Child.Tick();
            }
            else
            {
                ExecuteAbortPolicy();

                return StatusCode.Failure;
            }
        }

        public override string ToString() => $"If: {isConditionMet}";
    }
}