using UnityEngine;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    [AddComponentMenu("AI/Behavior Tree Kit/Blackboard")]
    public class BlackboardComponent : MonoBehaviour, IBlackboard, IBlackboardComponent
    {
        protected virtual IBlackboard Blackboard { get; set; } = new Blackboard();

        public bool Contains<T>(string key) => Blackboard.Contains<T>(key);

        public void Set<T>(string key, T value) => Blackboard.Set<T>(key, value);

        public bool TryGet<T>(string key, out T value) => Blackboard.TryGet<T>(key, out value);

        public IBlackboard GetBlackboard() => Blackboard;

        public T Get<T>(string key) => Blackboard.Get<T>(key);

        public T Get<T>(string key, T defaultValue) => Blackboard.Get<T>(key, defaultValue);

        public bool Contains(string key) => Blackboard.Contains(key);

        public void Remove(string key) => Blackboard.Remove(key);
    }
}