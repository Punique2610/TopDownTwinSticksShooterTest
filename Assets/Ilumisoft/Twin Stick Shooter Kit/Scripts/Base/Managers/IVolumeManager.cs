namespace Ilumisoft.TwinStickShooterKit
{
    public interface IVolumeManager
    {
        float GetVolume(string parameterName);
        void SetVolume(string parameterName, float value);
    }
}