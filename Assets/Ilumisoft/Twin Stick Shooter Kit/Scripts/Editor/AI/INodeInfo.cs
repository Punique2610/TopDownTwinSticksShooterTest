namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface INodeInfo
    {
        /// <summary>
        /// The state of the node (none, running, success, failure, aborted)
        /// </summary>
        NodeState State { get; }

        /// <summary>
        /// The type of the node (composite, decorator, action, conditional)
        /// </summary>
        NodeType Type { get; }

        /// <summary>
        /// The depth level of the node in the tree
        /// </summary>
        int DepthLevel { get; }
    }
}