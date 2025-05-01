using System;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Base wrapper class for all blackboard entries
    /// </summary>
    public abstract class BlackboardEntry
    {
        public abstract Type GetValueType();
    }
}