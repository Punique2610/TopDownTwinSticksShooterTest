namespace Ilumisoft.TwinStickShooterKit
{
    public interface IManagerProvider
    {
        void Register(ManagerComponent manager);

        T Get<T>();
        bool TryGet<T>(out T manager);
    }
}