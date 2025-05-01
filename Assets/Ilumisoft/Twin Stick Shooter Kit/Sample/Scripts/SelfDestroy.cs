using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    /// <summary>
    /// Simple, self managed behaviour, which will destroy the game object it is attached to after a given amount of time.
    /// </summary>
    public class SelfDestroy : MonoBehaviour
    {
        [SerializeField]
        float time;

        float elapsedTime;

        private void OnEnable()
        {
            elapsedTime = 0.0f;
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > time)
            {
                Destroy(gameObject);
            }
        }
    }
}