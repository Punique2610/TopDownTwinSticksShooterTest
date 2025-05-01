namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks all children. 
    /// If all children return 'Success', this node returns 'Success' too.
    /// If all children return 'Failure', this node returns 'Failure' too.
    /// Otherwise this node returns 'Running'.
    /// </summary>
    public class ParallelNode : CompositeNode
    {
        public ParallelNode(AbortPolicy abortPolicy = AbortPolicy.Running) : base(abortPolicy) { }

        protected override StatusCode OnUpdate()
        {
            int successCount = 0;
            int failureCount = 0;

            // Tick all children and count the number of 'Success' and 'Failure' results
            for (int i = 0; i < Children.Count; i++)
            {
                var child = Children[i];

                var statusCode = child.Tick();

                if (statusCode == StatusCode.Success)
                {
                    successCount++;
                }

                if (statusCode == StatusCode.Failure)
                {
                    failureCount++;
                }
            }

            // Return 'Success' if all children have returned 'Success'
            if (successCount == Children.Count)
            {
                return StatusCode.Success;
            }

            // Return 'Failure' if all children have returned 'Failure'
            if (failureCount == Children.Count)
            {
                return StatusCode.Failure;
            }

            return StatusCode.Running;
        }

        public override string ToString() => "Parallel";
    }
}