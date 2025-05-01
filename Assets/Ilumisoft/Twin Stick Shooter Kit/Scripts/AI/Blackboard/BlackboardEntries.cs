using System;
using System.Collections.Generic;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class BlackboardEntries
    {
        readonly Dictionary<string, BlackboardEntry> entries = new();

        BlackboardEntry<T> GetEntry<T>(string key)
        {
            return entries[key] as BlackboardEntry<T>;
        }

        public bool ContainsKey(string key) => entries.ContainsKey(key);

        public bool ContainsEntry<T>(string key)
        {
            return ContainsKey(key) && entries[key] is BlackboardEntry<T>;
        }

        public T GetValue<T>(string key)
        {
            if (ContainsKey(key))
            {
                return GetEntry<T>(key).Value;
            }

            return default;
        }

        public void SetValue<T>(string key, T value)
        {
            if (ContainsKey(key))
            {
                if (entries[key] is BlackboardEntry<T> entry)
                {
                    entry.Value = value;
                }
                else
                {
                    var currentEntry = entries[key];
                    throw new InvalidOperationException($"The requested type {typeof(T)} for key '{key}' does not match the type of the current entry {currentEntry.GetValueType()}");
                }
            }
            else
            {
                entries.Add(key, new BlackboardEntry<T> { Value = value });
            }
        }

        public void Remove(string key)
        {
            entries.Remove(key);
        }
    }
}