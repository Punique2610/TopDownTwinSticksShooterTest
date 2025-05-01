using System.Collections.Generic;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface ICompositeNode : INode
    {
        public AbortPolicy AbortPolicy { get; set; }

        int ChildCount { get; }

        List<Node> Children { get; }

        void AbortChildren();
        void AddChild(Node child);
        void ClearChildren();
        Node GetChild(int index);
        void RemoveChild(Node child);
    }
}