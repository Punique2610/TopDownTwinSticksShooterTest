using System.Collections.Generic;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface IBehaviorTreeInfo
    {
        IEnumerable<INodeInfo> Nodes { get; }
    }
}