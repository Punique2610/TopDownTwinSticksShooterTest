using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class SparkVFX : MonoBehaviour, ISpawnCallbackReceiver
    {
        [SerializeField]
        ParticleSystem sparkParticles;

        [SerializeField]
        float lifetime;

        float timeElapsed;

        bool isDestroyed = false;

        ISpawningManager SpawningManager { get; set; }

        private void Awake()
        {
            SpawningManager = Managers.Get<ISpawningManager>();
        }

        public void OnSpawn()
        {
            sparkParticles.Play();

            timeElapsed = 0.0f;

            isDestroyed = false;
        }

        private void Update()
        {
            if (isDestroyed)
            {
                return;
            }

            timeElapsed += Time.deltaTime;

            if (timeElapsed > lifetime)
            {
                SpawningManager.Despawn(gameObject);
                isDestroyed = true;
            }
        }
    }
}