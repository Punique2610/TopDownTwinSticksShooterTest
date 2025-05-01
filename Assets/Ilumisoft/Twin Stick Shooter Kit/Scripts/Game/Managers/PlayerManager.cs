using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Player Manager
    /// </summary>
    public class PlayerManager : ManagerComponent, IPlayerManager
    {
        GameObject player = null;

        public override void OnInitialize()
        {
            base.OnInitialize();
        }

        public GameObject GetPlayer()
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }

            return player;
        }

        public bool TryGetPlayer(out GameObject player)
        {
            player = GetPlayer();

            return player != null;
        }
    }
}