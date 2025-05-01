namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks the child and returns the inverted result:
    /// If the child returns 'Success' the inverter returns 'Failure'
    /// If the child returns 'Failure' the inverter returns 'Success'
    /// If the child returns 'Running' the inverter returns 'Running' too
    /// </summary>
    public class InvertNode : DecoratorNode
    {
        protected override StatusCode OnUpdate()
        {
            return Child.Tick() switch
            {
                StatusCode.Success => StatusCode.Failure,
                StatusCode.Failure => StatusCode.Success,
                StatusCode.Running => StatusCode.Running,
                _ => throw new System.NotImplementedException(),
            };
        }

        public override string ToString() => "Invert";
    }
}