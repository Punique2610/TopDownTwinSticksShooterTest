using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public class WeaponInfoHUD : MonoBehaviour
    {
        [SerializeField]
        WeaponInfo infoUIPrefab;

        [SerializeField]
        Transform container;

        /// <summary>
        /// Reference to the actor system
        /// </summary>
        IPlayerManager PlayerManager { get; set; }

        /// <summary>
        /// Reference to the player GameObject
        /// </summary>
        GameObject Player { get; set; }

        /// <summary>
        /// Reference to the weapon controller of the player
        /// </summary>
        IWeaponControllerComponent WeaponController { get; set; }

        WeaponInfo weaponInfo;

        private void Awake()
        {
            PlayerManager = Managers.Get<IPlayerManager>();
        }

        private void Start()
        {
            Player = PlayerManager.GetPlayer();
            WeaponController = Player.GetComponent<IWeaponControllerComponent>();
            WeaponController.OnWeaponEquipped += OnWeaponEqipped;
        }

        private void OnWeaponEqipped()
        {
            // TODO
            var weapon = WeaponController.GetCurrentWeapon();
            SetWeaponInfo(weapon);

        }

        public void SetWeaponInfo(IWeaponComponent weapon)
        {
            if(weaponInfo == null)
            {
                weaponInfo = Instantiate(infoUIPrefab, container);
            }

            weaponInfo.BindWeapon(weapon);
        }
    }
}