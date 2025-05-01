namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Activates or deactivates the given game object, depending on the value returned by the given method.
    /// This node always return 'Success'
    /// </summary>
    public class SetActiveNode : ActionNode
    {
        /// <summary>
        /// The game object that should be activated/deactivated
        /// </summary>
        readonly GameObject gameObject;

        /// <summary>
        /// The method determining whether the object should be activated or deactivated
        /// </summary>
        readonly Func<bool> value;

        public SetActiveNode(GameObject gameObject, Func<bool> value)
        {
            this.gameObject = gameObject;
            this.value = value;

            // The given method cannot be null
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }

        protected override StatusCode OnUpdate()
        {
            if (gameObject != null)
            {
                gameObject.SetActive(value());
            }

            return StatusCode.Success;
        }

        public override string ToString()
        {
            return $"Set Active: {gameObject.name} = {value()}";
        }
    }
}