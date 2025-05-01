using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Message Broker.
    /// This gets created and injected into the Messages class automatically at startup.
    /// </summary>
    public class DefaultMessageBroker : MonoBehaviour, IMessageBroker
    {
        struct MessageChannel
        {
            public readonly object ChannelKey;

            public readonly Type MessageType;

            public MessageChannel(object channel, Type messageType)
            {
                ChannelKey = channel;
                MessageType = messageType;
            }
        }

        private readonly Dictionary<MessageChannel, HashSet<Delegate>> channelDictionary = new();

        public void Subscribe<TMessage>(object channel, Action<TMessage> listener)
        {
            var key = new MessageChannel(channel, typeof(TMessage));

            if (channelDictionary.TryGetValue(key, out var listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                // Create a new HashSet for the given type
                listeners = new HashSet<Delegate>
            {
                listener
            };

                // Add the has set to the dictionary
                channelDictionary.Add(key, listeners);
            }
        }

        public void Unsubscribe<TMessage>(object channel, Action<TMessage> listener)
        {
            var key = new MessageChannel(channel, typeof(TMessage));

            if (channelDictionary.TryGetValue(key, out var listeners))
            {
                listeners.Remove(listener);

                // Remove channel if no listeners exist anymore
                if (listeners.Count == 0)
                {
                    channelDictionary.Remove(key);
                }
            }
        }

        public void Publish<TMessage>(object channel, TMessage message)
        {
            var key = new MessageChannel(channel, typeof(TMessage));

            if (channelDictionary.TryGetValue(key, out var listeners))
            {
                foreach (var listener in listeners)
                {
                    if (listener is Action<TMessage> action)
                    {
                        action?.Invoke(message);
                    }
                }
            }
        }
    }
}