using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public struct MissionStateChangedMessage
    {
        public GameObject Sender;
        public MissionState MissionState;
    }
}