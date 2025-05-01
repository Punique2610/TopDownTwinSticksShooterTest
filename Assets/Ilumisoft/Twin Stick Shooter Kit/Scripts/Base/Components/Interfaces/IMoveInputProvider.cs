using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMoveInputProvider
    {
        Vector2 MoveInput { get; }
    }
}