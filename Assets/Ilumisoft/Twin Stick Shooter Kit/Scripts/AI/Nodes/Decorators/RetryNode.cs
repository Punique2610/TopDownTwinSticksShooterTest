namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Retry the child node up to n times until it returns 'Success':
    /// If the child returns 'Success', this node returns 'Success' too
    /// If the child returns 'Running', this node returns 'Running' too
    /// If all n iterations returned 'Failure', this node returns 'Failure'
    /// </summary>
    public class RetryNode : DecoratorNode
    {
        /// <summary>
        /// The number of time the child node should be repeated
        /// </summary>
        readonly int maxCount;

        /// <summary>
        /// The current number of times the child node has been repeated
        /// </summary>
        int currentCount;

        public RetryNode(int count)
        {
            maxCount = count;
        }

        protected override void OnStart()
        {
            // Reset the counter
            currentCount = 0;
        }

        protected override StatusCode OnUpdate()
        {
            // Repeat the child 'Count' times
            while (currentCount < maxCount)
            {
                switch (Child.Tick())
                {
                    case StatusCode.Success: return StatusCode.Success;
                    case StatusCode.Running: return StatusCode.Running;
                    default: break;
                }

                currentCount++;
            }

            // Return 'Failure' if all iterations completed with 'Failure'
            return StatusCode.Failure;
        }

        public override string ToString() => $"Retry: {currentCount}/{maxCount}";
    }
}