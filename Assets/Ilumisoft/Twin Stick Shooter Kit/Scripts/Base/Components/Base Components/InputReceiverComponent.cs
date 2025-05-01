using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    /// <summary>
    /// Abstract base class for the input receiver component
    /// </summary>
    public abstract class InputReceiverComponent : MonoBehaviour, IMoveInputProvider, ISprintInputProvider, ILookInputProvider
    {
        public abstract Vector2 MoveInput { get; protected set; }
        public abstract bool SprintInput { get; protected set; }
        public abstract Vector2 LookInput { get; protected set; }
    }
}