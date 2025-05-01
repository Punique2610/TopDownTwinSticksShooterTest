using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    [UnitCategory("Events\\Twin Stick Shooter Kit\\Game")]
    [UnitSurtitle("Health")]
    [UnitTitle("On Health Empty")]
    public sealed class HealthEmptyEventUnit : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnHealthEmpty;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Collectable")]
    [UnitSurtitle("Collectable")]
    [UnitTitle("On Collect")]
    public sealed class CollectEventListenerNode : GameObjectEventNode<GameObject>
    {
        public override string EventHook => EventNames.OnCollect;

        protected override string OutputValueLabel => "Collector";
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Fire Pressed")]
    public sealed class FirePressedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnFirePressed;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Fire Released")]
    public sealed class FireReleasedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnFireReleased;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Switch Weapon Pressed")]
    public sealed class SwitchWeaponPressedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnSwitchWeaponPressed;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Switch Weapon Released")]
    public sealed class SwitchWeaponReleasedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnSwitchWeaponReleased;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Sprint Pressed")]
    public sealed class SprintPressedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnSprintPressed;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Input")]
    [UnitSurtitle("Input")]
    [UnitTitle("On Sprint Released")]
    public sealed class SprintReleasedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnSprintReleased;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Spawning")]
    [UnitSurtitle("Spawning")]
    [UnitTitle("On Spawn")]
    public sealed class SpawnEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnSpawn;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Spawning")]
    [UnitSurtitle("Spawning")]
    [UnitTitle("On Despawn")]
    public sealed class DespawnEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnDespawn;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Game")]
    [UnitTitle("On Game Over")]
    public sealed class GameOverEventNode : EventNode<bool>
    {
        public override string EventHook => EventNames.OnGameOver;

        public override string Label => "Win";
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Mission")]
    [UnitTitle("On Mission Complete")]
    public sealed class MissionCompleteEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnMissionCompleted;
    }

    [UnitCategory("Events\\Twin Stick Shooter Kit\\Mission")]
    [UnitTitle("On Mission Failed")]
    public sealed class MissionFailedEventNode : GameObjectEventNode
    {
        public override string EventHook => EventNames.OnMissionFailed;
    }
}