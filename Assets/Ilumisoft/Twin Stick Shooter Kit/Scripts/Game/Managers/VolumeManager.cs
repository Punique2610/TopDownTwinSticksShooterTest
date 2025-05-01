using Ilumisoft.TwinStickShooterKit.Utilities;
using UnityEngine;
using UnityEngine.Audio;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Volume Manager
    /// </summary>
    public class VolumeManager : ManagerComponent, IVolumeManager
    {
        [SerializeField]
        AudioMixer audioMixer;

        public float GetVolume(string parameterName)
        {
            float volume = 1.0f;

            if (audioMixer.GetFloat(parameterName, out var value))
            {
                volume = AudioUtility.ConvertFromDecibel(value);
            }

            return volume;
        }

        public void SetVolume(string parameterName, float value)
        {
            var volume = Mathf.Clamp01(value);

            audioMixer.SetFloat(parameterName, AudioUtility.ConvertToDecibel(volume));
        }
    }
}