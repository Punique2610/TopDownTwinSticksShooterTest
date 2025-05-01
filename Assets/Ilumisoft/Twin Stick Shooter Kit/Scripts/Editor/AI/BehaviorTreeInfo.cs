using System.Collections.Generic;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Provides all info of a behavior tree required by the behavior tree viewer.
    /// It also simplifies the access by converting the tree structure into an enumerable list.
    /// </summary>
    public class BehaviorTreeInfo : IBehaviorTreeInfo
    {
        public IEnumerable<INodeInfo> Nodes {get; private set;}

        public BehaviorTreeInfo(IBehaviorTree behaviorTree)
        {
            Nodes = BuildNodeList(behaviorTree);
        }

        /// <summary>
        /// Builds a node info list for the given behavior tree
        /// </summary>
        /// <param name="behaviorTree"></param>
        List<INodeInfo> BuildNodeList(IBehaviorTree behaviorTree)
        {
            var list = new List<INodeInfo>();

            if (behaviorTree != null)
            {
                AddNodeRecursive(list, behaviorTree.Root, 0);
            }

            return list;
        }

        /// <summary>
        /// Recursive method adding the node to the list and all of it's children recursively
        /// </summary>
        /// <param name="node"></param>
        /// <param name="depthLevel"></param>
        void AddNodeRecursive(List<INodeInfo> list, INode node, int depthLevel)
        {
            // Cancel if the node is null
            if(node == null)
            {
                return;
            }

            // Add the node to the list
            list.Add(new NodeInfo(node, depthLevel));

            // If the node is a composite node, add all children recursively ( with depthLevel + 1)
            if(node is ICompositeNode compositeNode)
            {
                foreach(var child in compositeNode.Children)
                {
                    AddNodeRecursive(list, child, depthLevel + 1);
                }
            }

            // If the node is a decorator node, add it's child recursively ( with depthLevel + 1)
            if (node is IDecoratorNode decoratorNode)
            {
                AddNodeRecursive(list, decoratorNode.Child, depthLevel + 1);
            }
        }
    }
}