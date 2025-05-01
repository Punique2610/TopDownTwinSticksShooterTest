namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Base class of all nodes which represent branches in the tree, like  the selector, sequence and parallel node.
    /// </summary>
    public abstract class CompositeNode : Node, ICompositeNode
    {
        public CompositeNode(AbortPolicy abortPolicy = AbortPolicy.Running)
        {
            AbortPolicy = abortPolicy;
        }

        public AbortPolicy AbortPolicy { get; set; } = AbortPolicy.Running;

        public override NodeType Type => NodeType.Composite;

        /// <summary>
        /// List of all children of the node
        /// </summary>
        public List<Node> Children { get; private set; } = new List<Node>();

        /// <summary>
        /// Adds the given node to the children
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Node child)
        {
            Children.Add(child);
        }

        /// <summary>
        /// Removes the given node from the chidlren
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(Node child)
        {
            Children.Remove(child);
        }

        /// <summary>
        /// Clears all children
        /// </summary>
        public void ClearChildren()
        {
            Children.Clear();
        }

        /// <summary>
        /// Gets the number of children
        /// </summary>
        /// <returns></returns>
        public int ChildCount => Children.Count;

        /// <summary>
        /// Gets the child with the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node GetChild(int index)
        {
            return Children[index];
        }

        /// <summary>
        /// Callback invoked when the node gets aborted
        /// </summary>
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
                AbortChildren();
            }
        }

        /// <summary>
        /// Aborts all running children
        /// </summary>
        public void AbortChildren()
        {
            foreach (var child in Children)
            {
                if (child.State == NodeState.Running)
                {
                    child.Abort();
                }
            }
        }

        public override void ResetState()
        {
            base.ResetState();

            foreach (var child in Children)
            {
                if (child.State != NodeState.Running)
                {
                    child.ResetState();
                }
            }
        }

        public override void SetBlackboard(IBlackboard blackboard)
        {
            base.SetBlackboard(blackboard);

            foreach (var child in Children)
            {
                child.SetBlackboard(blackboard);
            }
        }

        public override void SetOwner(GameObject owner)
        {
            base.SetOwner(owner);

            foreach (var child in Children)
            {
                child.SetOwner(owner);
            }
        }
    }
}