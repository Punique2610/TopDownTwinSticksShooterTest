namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks all of it's child nodes one by another until one returns 'Success' or 'Running'.
    /// If all children returned 'Failure' this node returns 'Failure' too.
    /// Unlike the selector node the reactive selector has no memory.
    /// </summary>
    public class ReactiveSelectorNode : CompositeNode
    {
        public ReactiveSelectorNode(AbortPolicy abortPolicy = AbortPolicy.Running) : base(abortPolicy) { }

        protected override void OnStart()
        {
            ResetState();
        }

        protected override StatusCode OnUpdate()
        {
            // Go thorugh all children
            foreach (var child in Children)
            {
                StatusCode status = child.Tick();

                if (status == StatusCode.Success)
                {
                    ExecuteAbortPolicy();

                    return status;
                }

                if (status == StatusCode.Running)
                {
                    return status;
                }
            }

            // If all children returned 'Failure', return 'Failure' too
            return StatusCode.Failure;
        }

        public override string ToString() => "Reactive Selector";
    }
}