namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Base class of all condition nodes.
    /// Condition nodes should not perform any actions and therefore only return 'Success' or 'Failure'
    /// </summary>
    public abstract class ConditionNode : Node, IConditionNode
    {
        public override NodeType Type => NodeType.Condition;
    }
}