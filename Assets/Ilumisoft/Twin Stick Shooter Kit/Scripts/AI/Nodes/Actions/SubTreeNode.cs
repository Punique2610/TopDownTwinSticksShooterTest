namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Allows to add a sub tree. 
    /// The sub tree ticks the given behavior tree and returns it's status code.
    /// </summary>
    public class SubTreeNode : ActionNode
    {
        readonly IBehaviorTree behaviorTree;

        public SubTreeNode(IBehaviorTree tree)
        {
            this.behaviorTree = tree;
        }

        protected override StatusCode OnUpdate()
        {
            return behaviorTree.Tick();
        }

        public override void SetBlackboard(IBlackboard blackboard)
        {
            base.SetBlackboard(blackboard);
            behaviorTree.SetBlackboard(blackboard);
        }

        public override string ToString()
        {
            return "Sub Tree: Tick";
        }
    }
}