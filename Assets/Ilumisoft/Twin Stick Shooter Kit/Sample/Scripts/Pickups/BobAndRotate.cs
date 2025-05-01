using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    /// <summary>
    /// Simple, self managed behaviour, which will animate position bobbing and rotation of the game object it is attached to.
    /// </summary>
    public class BobAndRotate : MonoBehaviour
    {
        [Tooltip("Frequency at which the item will move up and down")]
        public float VerticalBobFrequency = 1f;

        [Tooltip("Distance the item will move up and down")]
        public float BobbingAmount = 1f;

        [Tooltip("Rotation angle per second")]
        public float RotatingSpeed = 360f;

        Vector3 startPosition;

        protected virtual void Start()
        {
            startPosition = transform.position;
        }

        void Update()
        {
            UpdateBobbing();
            UpdateRotation();
        }

        private void UpdateBobbing()
        {
            float bobbingAmount = (Mathf.Sin(Time.time * VerticalBobFrequency) * 0.5f + 0.5f) * BobbingAmount;
            transform.position = startPosition + Vector3.up * bobbingAmount;
        }

        private void UpdateRotation()
        {
            transform.Rotate(Vector3.up, RotatingSpeed * Time.deltaTime, Space.Self);
        }
    }
}