using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// This component gets added at runtime to game objects when they are spawned by the spawning manager.
    /// The spawning manager assigns the pool the game object will be returned to when they get despawned later on.
    /// </summary>
    [AddComponentMenu("")]
    public class Poolable : MonoBehaviour
    {
        /// <summary>
        /// Reference to the Object Pool, the game object will be returned to
        /// </summary>
        public IPool Pool { get; set; }
    }
}