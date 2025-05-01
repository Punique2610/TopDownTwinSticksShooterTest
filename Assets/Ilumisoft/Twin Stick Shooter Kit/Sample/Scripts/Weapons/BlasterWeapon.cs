using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class BlasterWeapon : WeaponComponent, IUseAmmo, IAimableWeapon, IWeaponIcon
    {
        [Header("Control")]
        public bool IsControlledByAI = false;

        /// <summary>
        /// Current amount of ammo this weapon has
        /// </summary>
        [Header("Stats"), SerializeField]
        private int ammo = 100;

        /// <summary>
        /// Max ammount of ammo this weapon can hold
        /// </summary>
        [SerializeField]
        private int maxAmmo = 200;

        /// <summary>
        /// The damage caused by each shot
        /// </summary>
        [SerializeField]
        private float damage = 10.0f;

        /// <summary>
        /// The delay between each shot.
        /// This defines the fire frequency of the weapon
        /// </summary>
        [SerializeField]
        private float delayBetweenShots = 0.1f;

        /// <summary>
        /// Max angle a projectile direction might be scattered when shooting.
        /// Basically this defines the accuracy of the weapon
        /// </summary>
        [Range(0, 90)]
        public float MaxScatterAngle = 0.0f;

        /// <summary>
        /// How fast the weapon needs to aim at an enemy in seconds
        /// </summary>
        [SerializeField]
        public float AimSpeed = 1.0f;

        /// <summary>
        /// Reference to the prefab of the balster projectile
        /// </summary>
        [Header("Projectile")]
        public BlasterProjectile BlasterProjectilePrefab;

        /// <summary>
        /// Reference to the muzzle transform
        /// </summary>
        [Header("Muzzle")]
        public Transform Muzzle = null;

        /// <summary>
        /// Reference to the audio clip played when the weapon fires
        /// </summary>
        [Header("Audio")]
        public AudioClip FireSFX = null;

        /// <summary>
        /// Reference to the icon visible in the UI
        /// </summary>
        [Header("Icon")]
        public Sprite Icon = null;

        /// <summary>
        /// Gest or sets the damage the weapon causes with each hit
        /// </summary>
        public float Damage { get => damage; set => damage = value; }

        /// <summary>
        /// Event invoked when the amount of ammo has changed
        /// </summary>
        public UnityAction OnAmmoAmountChanged { get; set; }

        /// <summary>
        /// Gets or sets the current amount of ammo
        /// </summary>
        public int Ammo { get => ammo; set => ammo = value; }

        /// <summary>
        /// Gets or sets the max ammount of ammo you can collect for this weapon
        /// </summary>
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }

        /// <summary>
        /// Timer used to determine whether the delay after a shot is expired
        /// </summary>
        Timer DelayBetweenShotsTimer { get; set; } = new Timer();

        /// <summary>
        /// Reference to the AudioSource component
        /// </summary>
        AudioSource AudioSource { get; set; }

        ISpawningManager SpawningManager { get; set; }

        private void Awake()
        {
            SpawningManager = Managers.Get<ISpawningManager>();
            AudioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            DelayBetweenShotsTimer.Start(delayBetweenShots);
        }

        void Update()
        {
            // Update the delay timer
            DelayBetweenShotsTimer.Tick(Time.deltaTime);

            // Check whether the weapon can fire
            if (CanFire())
            {
                Fire();

                // Restart the delay timer
                DelayBetweenShotsTimer.Restart();
            }
        }

        /// <summary>
        /// Returns true if the delay is expired, the trigger is down and the weapon has ammo left
        /// </summary>
        /// <returns></returns>
        bool CanFire()
        {
            return DelayBetweenShotsTimer.IsElapsed && IsTriggerDown && (Ammo > 0 || IsControlledByAI);
        }

        /// <summary>
        /// Fires a single shot
        /// </summary>
        void Fire()
        {
            // AI does not use ammo, so only reduce the amount of ammo if the weapon is not controlled by AI
            if (!IsControlledByAI)
            {
                Ammo--;
                OnAmmoAmountChanged?.Invoke();
            }

            OnFire?.Invoke();

            SpawnProjectile(Muzzle.position, GetShotDirectionWithinSpread(Muzzle));

            PlayFireSFX();
        }

        /// <summary>
        /// Plays the fire soundeffect
        /// </summary>
        private void PlayFireSFX()
        {
            if (AudioSource != null && FireSFX != null)
            {
                AudioSource.PlayOneShot(FireSFX);
            }
        }

        /// <summary>
        /// Gets a random direction
        /// </summary>
        /// <param name="shootTransform"></param>
        /// <returns></returns>
        public Vector3 GetShotDirectionWithinSpread(Transform shootTransform)
        {
            return Vector3.Slerp(shootTransform.forward, Random.insideUnitSphere, MaxScatterAngle / 180f);
        }

        /// <summary>
        /// Spawns a projectile
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        private void SpawnProjectile(Vector3 position, Vector3 direction)
        {
            var projectile = SpawningManager.Spawn(BlasterProjectilePrefab.gameObject).GetComponent<BlasterProjectile>();

            projectile.transform.position = position;
            projectile.transform.forward = direction;
            projectile.damage = damage;

            projectile.Initialize(Owner);
        }

        /// <summary>
        /// Adds the given amount of ammo
        /// </summary>
        /// <param name="amount"></param>
        public void AddAmmo(int amount)
        {
            // Remember the previous amount of ammo
            int previousAmount = Ammo;

            // Calculate the new amount of ammo
            int newAmount = Mathf.Min(Ammo + amount, maxAmmo);

            // If ammo has been added, update the amount and invoke callback
            if (newAmount > previousAmount)
            {
                Ammo = newAmount;
                OnAmmoAmountChanged?.Invoke();
            }
        }

        /// <summary>
        /// Makes the weapon aim at the given target
        /// </summary>
        /// <param name="aimTarget"></param>
        public void Aim(Vector3 aimTarget)
        {
            Vector3 lookDirection = (aimTarget - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * AimSpeed);
        }

        public Sprite GetIcon()
        {
            return Icon;
        }
    }
}