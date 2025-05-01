namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// If the child node returns 'Running', this node will return 'Running' too. 
    /// Otherwise it will return 'Success'.
    /// </summary>
    public class ForceSuccessNode : DecoratorNode
    {
        protected override StatusCode OnUpdate()
        {
            return Child.Tick() switch
            {
                StatusCode.Success or StatusCode.Failure => StatusCode.Success,
                StatusCode.Running => StatusCode.Running,
                _ => throw new System.NotImplementedException(),
            };
        }

        public override string ToString() => "Force Success";
    }
}