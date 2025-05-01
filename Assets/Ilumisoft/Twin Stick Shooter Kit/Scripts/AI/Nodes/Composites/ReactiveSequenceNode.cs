namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks all of it's child nodes one by another until one returns 'Failure' or 'Running'.
    /// If all children returned 'Success' this node returns 'Success' too.
    ///  Unlike the sequence node the reactive sequence has no memory.
    /// </summary>
    public class ReactiveSequenceNode : CompositeNode
    {
        public ReactiveSequenceNode(AbortPolicy abortPolicy = AbortPolicy.Running) : base(abortPolicy) { }

        protected override void OnStart()
        {
            ResetState();
        }

        protected override StatusCode OnUpdate()
        {
            foreach (var child in Children)
            {
                StatusCode status = child.Tick();

                // If the child returned 'Running', return 'Running' too
                if (status == StatusCode.Running)
                {
                    return StatusCode.Running;
                }

                // If the child returned 'Failure', return 'Failure'
                if (status == StatusCode.Failure)
                {
                    ExecuteAbortPolicy();

                    return StatusCode.Failure;
                }
            }

            // If all children returned 'Success', return 'Success' too
            return StatusCode.Success;
        }

        public override string ToString() => "Reactive Sequence";
    }
}