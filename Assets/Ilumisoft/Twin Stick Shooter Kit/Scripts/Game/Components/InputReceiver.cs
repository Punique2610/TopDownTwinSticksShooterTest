using UnityEngine;
using UnityEngine.InputSystem;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    public class InputReceiver : InputReceiverComponent
    {
        public bool IsGamepad { get; protected set; } = false;

        public override Vector2 MoveInput { get; protected set; }

        public override Vector2 LookInput { get; protected set; }

        public override bool SprintInput { get; protected set; }

        public bool FireInput { get; private set; }

        public virtual void OnControlsChanged(PlayerInput input)
        {
            IsGamepad = input.currentControlScheme == "Gamepad";
        }

        public virtual void OnMove(InputValue value)
        {
            MoveInput = value.Get<Vector2>();
        }

        public virtual void OnLook(InputValue value)
        {
            if (!IsGamepad)
            {
                // The mouse screen position
                var mousePosition = value.Get<Vector2>();

                // Create a plane using the players y position
                var plane = new Plane(Vector3.up, transform.position.y);

                // Convert mouse position to world position on player plane
                var mouseWorldPosition = MousePositionToWorldPlane(mousePosition, plane);

                // Get the direction between player and mouse
                var direction = mouseWorldPosition - transform.position;

                // Use the x,z values to set the look
                LookInput = new Vector2(direction.x, direction.z);
            }
            else
            {
                LookInput = value.Get<Vector2>();
            }
        }

        public virtual void OnFire(InputValue value)
        {
            FireInput = value.isPressed;

            Messages.Publish(new FireInputChangedMessage
            {
                Sender = gameObject,
                IsPressed = value.isPressed
            });
        }

        public virtual void OnSprint(InputValue value)
        {
            SprintInput = value.isPressed;
        }

        Vector3 MousePositionToWorldPlane(Vector2 mousePosition, Plane plane)
        {
            // Create a ray using the mouse position
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            // Get the position of the mouse on the plane
            if (plane.Raycast(ray, out var distance))
            {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }
    }
}