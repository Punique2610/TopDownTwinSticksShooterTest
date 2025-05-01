using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class BehaviorTree : IBehaviorTree
    {
        /// <summary>
        /// Reference to the owner of the behavior tree
        /// </summary>
        public GameObject Owner { get; private set; }

        /// <summary>
        /// Reference to the blackboard used by the behavior tree
        /// </summary>
        public IBlackboard Blackboard { get; private set; }

        /// <summary>
        /// Reference to the root node
        /// </summary>
        public INode Root { get; set; }

        /// <summary>
        /// Returns whether a bloackboard has been bound to the tree or not
        /// </summary>
        bool isBlackboardBound = false;

        public BehaviorTree(GameObject owner, IBlackboard blackboard, INode root)
        {
            Owner = owner;
            Blackboard = blackboard;
            Root = root;

            SetBlackboard(blackboard);
            SetOwner(owner);
        }

        public StatusCode Tick()
        {
            if (isBlackboardBound == false)
            {
                SetBlackboard(new Blackboard());
            }

            return Root.Tick();
        }

        /// <summary>
        /// Makes the behavior tree use the given blackboard
        /// </summary>
        /// <param name="blackboard"></param>
        public void SetBlackboard(IBlackboard blackboard)
        {
            isBlackboardBound = true;

            Blackboard = blackboard;

            if (Root != null)
            {
                Root.SetBlackboard(blackboard);
            }
        }

        public void SetOwner(GameObject owner)
        {
            Owner = owner;

            if(Root != null)
            {
                Root.SetOwner(owner);
            }
        }
    }
}