using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public struct HealthChangedMessage
    {
        public GameObject Sender;
        public float PreviousHealth;
        public float CurrentHealth;
        public float ChangeAmount;
    }
}