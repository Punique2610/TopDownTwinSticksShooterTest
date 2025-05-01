using System;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMessageBroker
    {
        void Publish<TMessage>(object channel, TMessage message);
        void Subscribe<TMessage>(object channel, Action<TMessage> listener);
        void Unsubscribe<TMessage>(object channel, Action<TMessage> listener);
    }
}