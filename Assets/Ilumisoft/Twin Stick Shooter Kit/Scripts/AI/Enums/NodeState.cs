namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public enum NodeState
    {
        /// <summary>
        /// Default state of the node when it has been reset or not yet started
        /// </summary>
        None,

        /// <summary>
        /// The node is running
        /// </summary>
        Running,

        /// <summary>
        /// The node completed with status 'Success'
        /// </summary>
        Success,

        /// <summary>
        /// The node completed with status 'Failure'
        /// </summary>
        Failure,

        /// <summary>
        /// The node has been aborted
        /// </summary>
        Aborted,
    }
}