using System;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public static class GameInitialization
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize()
        {
            var startupConfiguration = StartupConfigurationAsset.Find();

            if(startupConfiguration == null)
            {
                QuitPlaymode();
                throw new Exception($"Could not find {typeof(StartupConfigurationAsset).Name} asset at {StartupConfigurationAsset.ConfigurationPath}. Did you delete, rename or move it?");
            }

            if (startupConfiguration.StartupProfile == null)
            {
                QuitPlaymode();
                throw new UnassignedReferenceException("No startup profile has been assigned. Please assign a startup profile in the project settings.");
            }
            
            startupConfiguration.StartupProfile.Initialize();
        }

        static void QuitPlaymode()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}