using System;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Template class used to create entries
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BlackboardEntry<T> : BlackboardEntry
    {
        /// <summary>
        /// The value of the entry
        /// </summary>
        public T Value;

        /// <summary>
        /// The type of the entry
        /// </summary>
        /// <returns></returns>
        public override Type GetValueType()
        {
            return typeof(T);
        }
    }
}