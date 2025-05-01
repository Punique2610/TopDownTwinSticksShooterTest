using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    [CreateAssetMenu()]
    public class StartupConfigurationAsset : ScriptableObject
    {
        public const string ConfigurationPath = "Ilumisoft/Twin Stick Shooter Kit/Startup Configuration";

        [SerializeField]
        private StartupProfileAsset startupProfile = null;

        public StartupProfileAsset StartupProfile
        {
            get => startupProfile;
            set => startupProfile = value;
        }

        public static StartupConfigurationAsset Find()
        {
            var result = Resources.Load<StartupConfigurationAsset>(ConfigurationPath);

            return result;
        }
    }
}