using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : PlayerControllerComponent
    {
        [Header("Settings"), SerializeField]
        private bool isControllable = true;

        [Header("Movement")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Header("Orientation")]
        public float OrientationSpeed = 5.0f;

        /// <summary>
        /// Reference to the Character Controller component
        /// </summary>
        public CharacterController CharacterController { get; private set; }

        /// <summary>
        /// The current speed of the character
        /// </summary>
        public float Speed { get; private set; }

        /// <summary>
        /// Gets or sets whether the character is controllable
        /// </summary>
        public override bool IsControllable { get => isControllable; set => isControllable = value; }

        /// <summary>
        /// Reference to the Animator component
        /// </summary>
        protected Animator Animator { get; set; }

        private float targetRotation = 0.0f;

        private void Awake()
        {
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
        }

        public override void Move(Vector2 moveInput, bool isSprinting)
        {
            // Only move the player if it is controllable and has move input
            if (!IsControllable || moveInput == Vector2.zero)
            {
                return;
            }

            // Accelerate the current speed towards the desired target speed
            Speed = AccelerateTowards(GetCurrentSpeed(), GetTargetSpeed(moveInput, isSprinting), SpeedChangeRate * Time.deltaTime);

            // Move the character
            CharacterController.Move(GetTargetDirection(moveInput) * (Speed * Time.deltaTime));
        }

        /// <summary>
        /// Accelerates the given current value towards the given target using the iven change rate.
        /// The result is rounded to 3 decimal places.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="changeRate"></param>
        /// <returns></returns>
        float AccelerateTowards(float current, float target, float changeRate)
        {
            float result = Mathf.Lerp(current, target, changeRate);
            return Mathf.Round(result * 1000f) / 1000f;
        }

        /// <summary>
        /// Gets the current speed of the player on the XZ plane
        /// </summary>
        /// <returns></returns>
        float GetCurrentSpeed()
        {
            return new Vector3(CharacterController.velocity.x, 0.0f, CharacterController.velocity.z).magnitude;
        }

        /// <summary>
        /// Gets the direction the player should move to for the given move input
        /// </summary>
        /// <param name="moveInput"></param>
        /// <returns></returns>
        Vector3 GetTargetDirection(Vector2 moveInput)
        {
            moveInput.Normalize();

            targetRotation = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            return (Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward).normalized;
        }

        /// <summary>
        /// Gets the target speed the player is trying to reach
        /// </summary>
        /// <param name="moveInput"></param>
        /// <param name="isSprinting"></param>
        /// <returns></returns>
        float GetTargetSpeed(Vector2 moveInput, bool isSprinting)
        {
            float targetSpeed = isSprinting ? SprintSpeed : MoveSpeed;

            if (moveInput == Vector2.zero)
            {
                targetSpeed = 0.0f;
            }

            return targetSpeed;
        }

        public override void UpdateAnimations()
        {
            float x = 0;
            float y = 0;
            float speed = 0;

            if (IsControllable)
            {
                Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

                x = Vector3.Dot(targetDirection.normalized * Speed, transform.right);
                y = Vector3.Dot(targetDirection.normalized * Speed, transform.forward);
                speed = Speed;
            }

            if (Animator != null)
            {
                Animator.SetFloat("X", x);
                Animator.SetFloat("Y", y);
                Animator.SetFloat("Speed", speed);
            }
        }

        /// <summary>
        /// Rotates the player to the given look direction
        /// </summary>
        /// <param name="lookInput"></param>
        public override void Rotate(Vector2 lookInput)
        {
            if (!IsControllable)
            {
                return;
            }

            var lookDirection = new Vector3(lookInput.x, 0, lookInput.y).normalized;

            if (lookDirection != Vector3.zero)
            {
                OrientTowards(transform.position + lookDirection);
            }
        }

        /// <summary>
        /// Orients the player towards the given position
        /// </summary>
        /// <param name="lookPosition"></param>
        public void OrientTowards(Vector3 lookPosition)
        {
            Vector3 lookDirection = Vector3.ProjectOnPlane(lookPosition - transform.position, Vector3.up).normalized;

            if (lookDirection.sqrMagnitude != 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * OrientationSpeed);
            }
        }
    }
}