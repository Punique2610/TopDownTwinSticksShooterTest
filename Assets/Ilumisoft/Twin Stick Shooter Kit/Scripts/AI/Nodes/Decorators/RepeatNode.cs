namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Repeat the child node n times:
    /// If the child returns 'Failure', this node returns 'Failure' too
    /// If the child returns 'Running', this node returns 'Running' too
    /// If all n iterations returned 'Success', this node returns 'Success'
    /// </summary>
    public class RepeatNode : DecoratorNode
    {
        /// <summary>
        /// The number of time the child node should be repeated
        /// </summary>
        readonly int maxCount;

        /// <summary>
        /// The current number of times the child node has been repeated
        /// </summary>
        int currentCount;

        public RepeatNode(int count)
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
                    case StatusCode.Failure: return StatusCode.Failure;
                    case StatusCode.Running: return StatusCode.Running;
                    default: break;
                }

                currentCount++;
            }

            // Return 'Success' if all iterations completed with 'Success'
            return StatusCode.Success;
        }

        public override string ToString() => $"Repeat: {currentCount}/{maxCount}";
    }
}