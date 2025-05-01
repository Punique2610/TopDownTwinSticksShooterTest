namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;

    /// <summary>
    /// Checks whether the given condition is true. If so the node returns 'Success'. 
    /// Otherwise it returns 'Failure'
    /// </summary>
    public class IsConditionMetNode : ConditionNode
    {
        /// <summary>
        /// Reference to the method used to determine whether the condition is met or not
        /// </summary>
        readonly Func<bool> isConditionMet = null;
        
        /// <summary>
        /// The name of the node
        /// </summary>
        readonly string name = string.Empty;

        public IsConditionMetNode(string name, Func<bool> isConditionMet)
        {
            this.name = name;
            this.isConditionMet = isConditionMet;
        }

        protected override StatusCode OnUpdate()
        {
            return IsConditionMet() ? StatusCode.Success : StatusCode.Failure;
        }

        bool IsConditionMet()
        {
            return isConditionMet != null && isConditionMet();
        }

        public override string ToString() => name;
    }
}