namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks all of it's child nodes one by another until one returns 'Failure' or 'Running'.
    /// If all children returned 'Success' this node returns 'Success' too.
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        /// <summary>
        /// The index of the child node which is curretnly processed
        /// </summary>
        int currentIndex = 0;

        protected override void OnStart()
        {
            currentIndex = 0;

            ResetState();
        }

        protected override StatusCode OnUpdate()
        {
            while (true)
            {
                StatusCode status = Children[currentIndex].Tick();

                // If the child returned 'Running', return 'Running' too
                // If the child returned 'Failure', return 'Failure'
                if (status == StatusCode.Running || status == StatusCode.Failure)
                {
                    return status;
                }

                // If the current child returned 'Success', continue with the next one
                currentIndex++;

                // If all children returned 'Success', return 'Success' too
                if (currentIndex == Children.Count)
                {
                    return StatusCode.Success;
                }
            }
        }

        public override string ToString() => "Sequence";
    }
}