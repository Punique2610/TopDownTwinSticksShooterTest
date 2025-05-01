using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public class VisualScriptingMessageHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            Messages.Subscribe<ObjectCollectedMessage>(OnObjectCollected);
            Messages.Subscribe<FireInputChangedMessage>(OnFireInputChanged);
            Messages.Subscribe<ObjectSpawnedMessage>(OnObjectSpawned);
            Messages.Subscribe<ObjectDespawnedMessage>(OnObjectDespawned);
            Messages.Subscribe<MissionStateChangedMessage>(OnMissionStateChanged);
            Messages.Subscribe<HealthChangedMessage>(OnHealthChanged);
            Messages.Subscribe<GameOverMessage>(OnGameOver);
        }

        private void OnDisable()
        {
            Messages.Unsubscribe<ObjectCollectedMessage>(OnObjectCollected);
            Messages.Unsubscribe<FireInputChangedMessage>(OnFireInputChanged);
            Messages.Unsubscribe<ObjectSpawnedMessage>(OnObjectSpawned);
            Messages.Unsubscribe<ObjectDespawnedMessage>(OnObjectDespawned);
            Messages.Unsubscribe<MissionStateChangedMessage>(OnMissionStateChanged);
            Messages.Unsubscribe<HealthChangedMessage>(OnHealthChanged);
            Messages.Unsubscribe<GameOverMessage>(OnGameOver);
        }

        private void OnMissionStateChanged(MissionStateChangedMessage message)
        {
            switch (message.MissionState)
            {
                case MissionState.Completed:
                    EventBus.Trigger(EventNames.OnMissionCompleted, message.Sender);
                    break;
                case MissionState.Failed:
                    EventBus.Trigger(EventNames.OnMissionFailed, message.Sender);
                    break;
                default:
                    break;
            }
        }

        private void OnHealthChanged(HealthChangedMessage message)
        {
            if (message.CurrentHealth == 0.0f)
            {
                EventBus.Trigger(EventNames.OnHealthEmpty, message.Sender);
            }
        }

        private void OnGameOver(GameOverMessage message)
        {
            EventBus.Trigger(EventNames.OnGameOver, message.HasWon);
        }

        private void OnObjectDespawned(ObjectDespawnedMessage message)
        {
            EventBus.Trigger(EventNames.OnSpawn, message.Sender);
        }

        private void OnObjectSpawned(ObjectSpawnedMessage message)
        {
            EventBus.Trigger(EventNames.OnSpawn, message.Sender);
        }

        private void OnFireInputChanged(FireInputChangedMessage message)
        {
            if (message.IsPressed)
            {
                EventBus.Trigger(EventNames.OnFirePressed, message.Sender);
            }
            else
            {
                EventBus.Trigger(EventNames.OnFireReleased, message.Sender);
            }
        }

        private void OnObjectCollected(ObjectCollectedMessage message)
        {
            EventBus.Trigger(EventNames.OnCollect, message.Collectable, message.Collector);
        }
    }
}