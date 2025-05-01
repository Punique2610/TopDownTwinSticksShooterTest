using System;

namespace Ilumisoft.TwinStickShooterKit
{
    public static class Messages
    {
        public static IMessageBroker MessageBroker { get; set; }

        public static void Subscribe<TMessage>(Action<TMessage> listener)
        {
            MessageBroker.Subscribe(null, listener);
        }

        public static void Unsubscribe<TMessage>(Action<TMessage> listener)
        {
            MessageBroker.Unsubscribe(null, listener);
        }

        public static void Publish<TMessage>(TMessage message)
        {
            MessageBroker.Publish(null, message);
        }

        public static void Subscribe<TMessage>(object channel, Action<TMessage> listener)
        {
            MessageBroker.Subscribe(channel, listener);
        }

        public static void Unsubscribe<TMessage>(object channel, Action<TMessage> listener)
        {
            MessageBroker.Unsubscribe(channel, listener);
        }

        public static void Publish<TMessage>(object channel, TMessage message)
        {
            MessageBroker.Publish(channel, message);
        }
    }
}