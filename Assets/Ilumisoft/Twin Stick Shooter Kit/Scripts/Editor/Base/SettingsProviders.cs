using UnityEditor;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Editor
{
    class SettingsProviders
    {
        static SettingsProvider CreateProvider(string settingsWindowPath, Object asset)
        {
            var provider = AssetSettingsProvider.CreateProviderFromObject(settingsWindowPath, asset);

            provider.keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(asset));
            return provider;
        }

        [SettingsProvider]
        static SettingsProvider CreateStartupProfileConfigurationProvider() => CreateProvider("Project/Twin Stick Shooter Kit/Startup", StartupConfigurationAsset.Find());
    }
}