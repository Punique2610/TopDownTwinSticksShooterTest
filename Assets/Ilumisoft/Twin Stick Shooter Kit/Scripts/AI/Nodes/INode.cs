using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface INode
    {
        NodeState State { get; }

        NodeType Type { get; }

        void Abort();

        void SetOwner(GameObject owner);

        void SetBlackboard(IBlackboard blackboard);
        void ResetState();
        StatusCode Tick();
    }
}