namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface IDecoratorNode : INode
    {
        Node Child { get; set; }
    }
}