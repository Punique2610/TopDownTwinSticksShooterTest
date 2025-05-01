using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IPlayerControllerComponent : IComponent
    {
        void Move(Vector2 moveInput, bool isSprinting);
        void Rotate(Vector2 lookInput);
    }
}