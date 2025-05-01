namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    /// <summary>
    /// Checks whether an entry exists with the given key and returns 'Success' when true,
    /// 'Failure' otherwise
    /// </summary>
    public class ContainsBlackboardEntry : ConditionNode
    {
        readonly string key;

        public ContainsBlackboardEntry(string key)
        {
            this.key = key;
        }
        protected override StatusCode OnUpdate()
        {
            return Blackboard.Contains(key) ? StatusCode.Success : StatusCode.Failure;
        }

        public override string ToString() => $"Contains Blackboard Entry: {key}";
    }

    /// <summary>
    ///  Checks whether an entry of a T exists with the given key and returns 'Success' when true,
    /// 'Failure' otherwise
    /// </summary>
    public class ContainsBlackboardEntry<T> : ConditionNode
    {
        readonly string key;

        public ContainsBlackboardEntry(string key)
        {
            this.key = key;
        }

        protected override StatusCode OnUpdate()
        {
            return Blackboard.Contains<T>(key) ? StatusCode.Success : StatusCode.Failure;
        }

        public override string ToString() => $"Contains Blackboard Entry ({typeof(T)}): {key}";
    }
}