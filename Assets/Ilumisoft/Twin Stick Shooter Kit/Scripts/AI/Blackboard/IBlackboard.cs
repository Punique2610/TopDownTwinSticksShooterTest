namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public interface IBlackboard
    {
        bool Contains(string key);
        bool Contains<T>(string key);
        void Set<T>(string key, T value);
        bool TryGet<T>(string key, out T value);
        T Get<T>(string key);
        T Get<T>(string key, T defaultValue);
        void Remove(string key);
    }
}