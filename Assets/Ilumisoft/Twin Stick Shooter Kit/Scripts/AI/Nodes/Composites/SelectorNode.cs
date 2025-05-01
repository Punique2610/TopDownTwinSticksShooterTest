namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Ticks all of it's child nodes one by another until one returns 'Success' or 'Running'.
    /// If all children returned 'Failure' this node returns 'Failure' too.
    /// </summary>
    public class SelectorNode : CompositeNode
    {
        /// <summary>
        /// The index of the child node which is curretnly processed
        /// </summary>
        protected int currentIndex = 0;

        protected override void OnStart()
        {
            currentIndex = 0;

            ResetState();
        }

        protected override StatusCode OnUpdate()
        {
            while (true)
            {
                // Tick the currently active child
                StatusCode status = Children[currentIndex].Tick();

                // If the child returned 'Running', return 'Running' too
                // If the child returned 'Success', return 'Success'
                if (status == StatusCode.Running || status == StatusCode.Success)
                {
                    return status;
                }

                // If the current child returned 'Failure', continue with the next one
                currentIndex++;

                // If all children have returned 'Failure', return 'Failure' too
                if (currentIndex == Children.Count)
                {
                    return StatusCode.Failure;
                }
            }
        }

        public override string ToString() => "Selector";
    }
}