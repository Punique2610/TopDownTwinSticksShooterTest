using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    ///  Default implementation of the weapon controller component.
    ///  It allows you to define a default weapon for the player and handles aiming of the weapon automatically.
    /// </summary>
    [AddComponentMenu("Twin Stick Shooter Kit/Game/Weapon Controller")]
    public class WeaponController : WeaponControllerComponent
    {
        /// <summary>
        /// Parent socket holding all weapons controlled by this component
        /// </summary>
        [Header("Weapon")]
        [Tooltip("Parent container for all weapons")]
        public Transform WeaponSocket = null;

        /// <summary>
        /// The default weapon instantiated when the game is started. This is optional and can be null
        /// </summary>
        [SerializeField]
        WeaponComponent defaultWeaponPrefab = null;

        [Header("Aiming")]
        [SerializeField, Tooltip("Use the offset to fine tune from where the aim raycast should be shot. Make sure this is not intersecting with the actor itself.")]
        Vector3 aimOffset = Vector3.zero;

        [SerializeField, Tooltip("The min distance the weapon can aim at. Setting this too low might result in a weird look")]
        float minAimingDistance = 2.0f;

        [SerializeField, Tooltip("The max distance the weapon can aim at")]
        float maxAimingDistance = 20.0f;

        /// <summary>
        /// The active weapon used
        /// </summary>
        public IWeaponComponent CurrentWeapon { get; set; }

        private void Awake()
        {
            // Equip the default weapon, if one has been set
            if (defaultWeaponPrefab != null)
            {
                Equip(defaultWeaponPrefab);
            }
        }

        private void Update()
        {
            UpdateAiming();
        }

        private void OnDrawGizmos()
        {
            DrawAimingGizmos();
        }

        /// <summary>
        /// Draw gizmos visualising the point to aim at in the scene view
        /// </summary>
        void DrawAimingGizmos()
        {
            var aimPoint = GetAimPoint();

            Gizmos.color = Color.red;
            Debug.DrawLine(GetAimOrigin(), aimPoint, Color.red);
            Gizmos.DrawWireSphere(aimPoint, 0.2f);
        }

        /// <summary>
        /// Makes the current weapon aim at the aim point.
        /// </summary>
        void UpdateAiming()
        {
            if (!CurrentWeapon.IsNullOrDestroyed() && CurrentWeapon.TryGetComponent<IAimableWeapon>(out var aimable))
            {
                aimable.Aim(GetAimPoint());
            }
        }

        /// <summary>
        /// Returns the point to aim at. A raycast is shot from the aim origin in the forward direction of the player. The hit point is used as the point to aim at
        /// </summary>
        /// <returns></returns>
        Vector3 GetAimPoint()
        {
            Vector3 aimPoint = GetAimPoint(maxAimingDistance);

            Vector3 aimOrigin = GetAimOrigin();

            if (Physics.Raycast(aimOrigin, transform.forward, out var hitInfo, maxAimingDistance))
            {
                if (Vector3.Distance(aimOrigin, hitInfo.point) < minAimingDistance)
                {
                    aimPoint = GetAimPoint(minAimingDistance);
                }
                else
                {
                    aimPoint = hitInfo.point;
                }
            }

            return aimPoint;
        }

        /// <summary>
        /// Returns an aim point computed with the given distance from the aim origin
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        Vector3 GetAimPoint(float distance)
        {
            Vector3 aimPoint = GetAimOrigin() + transform.forward * distance;

            return aimPoint;
        }

        /// <summary>
        /// The origin point used to determine the point to aim at
        /// </summary>
        /// <returns></returns>
        Vector3 GetAimOrigin()
        {
            var offset = aimOffset.x * transform.right + aimOffset.y * transform.up + aimOffset.z * transform.forward;

            return transform.position + offset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="weaponPrefab"></param>
        public override void Equip(IWeaponComponent weaponPrefab)
        {
            if (!CurrentWeapon.IsNullOrDestroyed())
            {
                // Remove current weapon
                Destroy(CurrentWeapon.GameObject);
            }

            // Instantiate new weapon
            CurrentWeapon = Instantiate(weaponPrefab as MonoBehaviour, WeaponSocket) as IWeaponComponent;

            CurrentWeapon.Owner = gameObject;

            OnWeaponEquipped?.Invoke();
        }

        /// <summary>
        /// Starts firing with the currently equipped weapon
        /// </summary>
        public override void StartFiring()
        {
            if (!CurrentWeapon.IsNullOrDestroyed())
            {
                CurrentWeapon.PullTrigger();
            }
        }

        /// <summary>
        /// Stops firing with the currently equipped weapon
        /// </summary>
        public override void StopFiring()
        {
            if (!CurrentWeapon.IsNullOrDestroyed())
            {
                CurrentWeapon.ReleaseTrigger();
            }
        }

        public override IWeaponComponent GetCurrentWeapon()
        {
            return CurrentWeapon;
        }
    }
}