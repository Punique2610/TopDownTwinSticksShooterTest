using System.Collections.Generic;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Compares the value of a blackbaord entry with a give value and returns 'Success' when both values are equal,
    /// 'Failure' otherwise
    /// </summary>
    public class CompareBlackboardEntry<T> : ConditionNode
    {
        readonly string key;

        T value;

        public CompareBlackboardEntry(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        protected override StatusCode OnUpdate()
        {
            return Blackboard.TryGet(key, out T res) && Compare(res, value) ? StatusCode.Success : StatusCode.Failure;
        }

        bool Compare(T x, T y)
        {
            return EqualityComparer<T>.Default.Equals(x, y);
        }

        public override string ToString() => $"Is Blackboard Entry ({typeof(T)}): {key}={value}";
    }
}