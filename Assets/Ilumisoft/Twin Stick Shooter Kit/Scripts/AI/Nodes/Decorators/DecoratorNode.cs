using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Base class of all decorator nodes. 
    /// A decorator has a single child node.
    /// </summary>
    public abstract class DecoratorNode : Node, IDecoratorNode
    {
        public AbortPolicy AbortPolicy { get; set; } = AbortPolicy.Running;

        public override NodeType Type => NodeType.Decorator;

        public Node Child { get; set; }

        public override void SetBlackboard(IBlackboard blackboard)
        {
            base.SetBlackboard(blackboard);

            Child.SetBlackboard(blackboard);
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            Child.SetOwner(owner);
        }


        protected override void OnAbort()
        {
            base.OnAbort();

            ExecuteAbortPolicy();
        }

        protected void ExecuteAbortPolicy()
        {
            // If the abort policy of the node is set to running, all of it's running children will be aborted as well
            if (AbortPolicy == AbortPolicy.Running)
            {
                AbortChild();
            }
        }

        /// <summary>
        /// Aborts the child node if it is running
        /// </summary>
        public void AbortChild()
        {
            if (Child.State == NodeState.Running)
            {
                Child.Abort();
            }
        }
    }
}