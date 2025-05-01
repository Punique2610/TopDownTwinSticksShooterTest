namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    public static class EventNames
    {
        public static string OnHealthEmpty { get; } = "Health Empty Event";

        public static string OnGameOver { get; } = "Game Over Event";

        public static string OnMissionCompleted { get; } = "Mission Completed Event";

        public static string OnMissionFailed { get; } = "Mission Failed Event";

        public static string OnDetectTarget { get; } = "Detect Target Event";

        public static string OnCollect { get; } = "On Collect Event";

        public static string OnSpawn { get; } = "On Spawn";

        public static string OnDespawn { get; } = "On Despawn";

        public static string OnFirePressed => "OnFirePressed";

        public static string OnFireReleased => "OnFireReleased";

        public static string OnSwitchWeaponPressed => "OnSwitchWeaponPressed";

        public static string OnSwitchWeaponReleased => "OnSwitchWeaponReleased";

        public static string OnSprintPressed => "OnSprintPressed";

        public static string OnSprintReleased => "OnSprintReleased";
    }
}