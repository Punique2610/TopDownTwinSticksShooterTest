using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public abstract class BehaviorTreeComponent : MonoBehaviour, IBehaviorTreeComponent
    {
        private IBehaviorTree BehaviorTree { get; set; } = null;

        protected abstract IBehaviorTree BehaviorTreeDefinition();

        protected void BuildTree()
        {
            BehaviorTree = BehaviorTreeDefinition();
        }

        public virtual void Start()
        {
            BuildTree();
        }

        public IBehaviorTree GetBehaviorTree()
        {
            return BehaviorTree;
        }

        public void Tick()
        {
            BehaviorTree.Tick();
        }

        public void Bind(IBlackboard blackboard)
        {
            BehaviorTree.SetBlackboard(blackboard);
        }
    }
}