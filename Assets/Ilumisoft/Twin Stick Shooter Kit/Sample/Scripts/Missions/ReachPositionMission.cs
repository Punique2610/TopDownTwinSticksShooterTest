using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Simple sample mission, which requries the player to reach a position.
    /// The mission succeeds if the player enters the trigger attached to the mission.
    /// </summary>
    [AddComponentMenu("Mission/Reach Position Mission", order: 2)]
    public class ReachPositionMission : MissionComponent
    {
        private void OnTriggerEnter(Collider other)
        {
            // Cancel if the mission is not active
            if (State != MissionState.Active)
            {
                return;
            }

            // Complete the mission if the player enters the attached trigger
            if (other.CompareTag("Player"))
            {
                CompleteMission();
            }
        }

        private void Reset()
        {
            // Automatically add a trigger collider if none has been added yet when the component is added or reset
            if(TryGetComponent<Collider>(out var _) == false)
            {
                var collider = gameObject.AddComponent<SphereCollider>();

                collider.isTrigger = true;
            }
        }
    }
}