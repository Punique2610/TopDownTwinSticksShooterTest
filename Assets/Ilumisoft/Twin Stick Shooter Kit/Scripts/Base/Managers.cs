namespace Ilumisoft.TwinStickShooterKit
{
    public static class Managers
    {
        public static IManagerProvider ManagerProvider { get; set; }

        public static void Register(ManagerComponent manager)
        {
            ManagerProvider.Register(manager);
        }

        public static T Get<T>()
        {
            return ManagerProvider.Get<T>();
        }

        public static bool TryGet<T>(out T manager)
        {
            return ManagerProvider.TryGet<T>(out manager);
        }
    }
}