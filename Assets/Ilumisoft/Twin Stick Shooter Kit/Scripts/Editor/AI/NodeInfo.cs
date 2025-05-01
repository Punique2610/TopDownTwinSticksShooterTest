namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Provides all info of a node required by the behavior tree viewer
    /// </summary>
    public class NodeInfo : INodeInfo
    {
        /// <summary>
        /// Reference to the node this info targets
        /// </summary>
        readonly INode node = null;

        /// <summary>
        /// The state of the node (none, running, success, failure, aborted)
        /// </summary>
        public NodeState State => node.State;

        /// <summary>
        /// The type of the node (composite, decorator, action, conditional)
        /// </summary>
        public NodeType Type => node.Type;

        /// <summary>
        /// The depth level of the node in the tree
        /// </summary>
        public int DepthLevel { get; private set; }

        public NodeInfo(INode node, int depthLevel)
        {
            this.node = node;
            DepthLevel = depthLevel;
        }

        public override string ToString()
        {
            return node.ToString();
        }
    }
}