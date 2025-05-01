using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface IBehaviorTree
    {
        IBlackboard Blackboard { get; }
        INode Root { get; set; }
        StatusCode Tick();
        void SetBlackboard(IBlackboard blackboard);
        void SetOwner(GameObject owner);
    }
}