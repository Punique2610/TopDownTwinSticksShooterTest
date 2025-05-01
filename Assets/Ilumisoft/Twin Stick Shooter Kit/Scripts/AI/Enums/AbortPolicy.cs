namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public enum AbortPolicy
    {
        /// <summary>
        /// Running nodes will not be aborted when the node stops running
        /// </summary>
        None,

        /// <summary>
        /// All running children will be aborted when the node stops running
        /// </summary>
        Running
    }
}