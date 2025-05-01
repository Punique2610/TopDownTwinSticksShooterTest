namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Base class for all action nodes. 
    /// Action nodes are leaf nodes and should not have any children.
    /// </summary>
    public abstract class ActionNode : Node, IActionNode
    {
        public override NodeType Type => NodeType.Action;
    }
}