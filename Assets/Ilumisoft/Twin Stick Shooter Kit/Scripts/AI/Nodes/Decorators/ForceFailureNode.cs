namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// If the child node returns 'Running', this node will return 'Running' too. 
    /// Otherwise it will return 'Failure'.
    /// </summary>
    public class ForceFailureNode : DecoratorNode
    {
        protected override StatusCode OnUpdate()
        {
            return Child.Tick() switch
            {
                StatusCode.Success or StatusCode.Failure => StatusCode.Failure,
                StatusCode.Running => StatusCode.Running,
                _ => throw new System.NotImplementedException(),
            };
        }

        public override string ToString() => "Force Failure";
    }
}