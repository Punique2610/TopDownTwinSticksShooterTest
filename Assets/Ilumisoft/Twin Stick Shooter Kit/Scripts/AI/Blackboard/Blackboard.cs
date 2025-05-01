namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class Blackboard : IBlackboard
    {
        readonly BlackboardEntries entries = new();

        public bool TryGet<T>(string key, out T value)
        {
            if (entries.ContainsEntry<T>(key))
            {
                value = entries.GetValue<T>(key);

                return true;
            }

            value = default;

            return false;
        }

        public bool Contains(string key) => entries.ContainsKey(key);

        public bool Contains<T>(string key) => entries.ContainsEntry<T>(key);

        public void Set<T>(string key, T value) => entries.SetValue(key, value);

        public T Get<T>(string key) => entries.GetValue<T>(key);

        public T Get<T>(string key, T defaultValue)
        {
            if(TryGet<T>(key, out var value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        public void Remove(string key) => entries.Remove(key);
    }
}