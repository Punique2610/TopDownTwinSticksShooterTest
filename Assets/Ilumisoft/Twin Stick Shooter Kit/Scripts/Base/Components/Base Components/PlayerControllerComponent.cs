using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the player component
    /// </summary>
    public abstract class PlayerControllerComponent : MonoBehaviour, IControllable, IPlayerControllerComponent, IPlayerAnimationController
    {
        public abstract bool IsControllable { get; set; }
        public GameObject GameObject => gameObject;

        public abstract void Move(Vector2 moveInput, bool isSprinting);
        public abstract void Rotate(Vector2 lookInput);
        public abstract void UpdateAnimations();
    }
}