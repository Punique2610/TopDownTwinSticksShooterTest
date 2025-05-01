using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public abstract class Node : INode
    {
        public static readonly INode Empty = new EmptyNode();

        bool isInitialized = false;

        /// <summary>
        /// Reference to the game object owning the behavior tree the node belongs to
        /// </summary>
        protected GameObject Owner { get; private set; }

        /// <summary>
        /// Reference to the blackboard
        /// </summary>
        protected IBlackboard Blackboard { get; private set; }

        /// <summary>
        /// The current state of the node
        /// </summary>
        public NodeState State { get; private set; } = NodeState.None;

        /// <summary>
        /// The type of the node (composite, decorator, action or condition)
        /// </summary>
        public abstract NodeType Type { get; }

        /// <summary>
        /// Callback invoked when the node gets ticked the first time. It will be called once, right before OnStart and can be used to gather references to required components
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Callback invoked when the node gets started. It will always be called right before OnUpdate when the node gets ticked and is not 'Running' yet
        /// </summary>
        protected virtual void OnStart() { }

        /// <summary>
        /// Callback invoked when the node is finished. This will always be called when the node completed OnUpdate with another state than 'Running'
        /// </summary>
        protected virtual void OnStop() { }

        /// <summary>
        /// Callback invoked when the node gets aborted
        /// </summary>
        protected virtual void OnAbort() { }

        /// <summary>
        /// Callback invoked when the node gets updated during the tick. This will be called every time the node gets ticked
        /// </summary>
        /// <returns></returns>
        protected virtual StatusCode OnUpdate()
        {
            return StatusCode.Success;
        }

        public StatusCode Tick()
        {
            if(!isInitialized)
            {
                OnInitialize();
                isInitialized = true;
            }

            if (State != NodeState.Running)
            {
                OnStart();
            }

            var statusCode = OnUpdate();

            State = statusCode switch
            {
                StatusCode.Success => NodeState.Success,
                StatusCode.Failure => NodeState.Failure,
                StatusCode.Running => NodeState.Running,
                _ => throw new System.NotImplementedException(),
            };

            if (State != NodeState.Running)
            {
                OnStop();
            }

            return statusCode;
        }

        public void Abort()
        {
            State = NodeState.Aborted;
            OnAbort();
        }

        public virtual void ResetState()
        {
            State = NodeState.None;
        }

        public virtual void SetBlackboard(IBlackboard blackboard)
        {
            Blackboard = blackboard;
        }

        public virtual void SetOwner(GameObject owner)
        {
            Owner = owner;
        }
    }
}