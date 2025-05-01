using Unity.VisualScripting;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.VisualScripting
{
    /// <summary>
    /// Node providing a reference to the player game object
    /// </summary>
    [UnitTitle("Player")]
    [UnitCategory("Twin Stick Shooter Kit\\Player")]
    [TypeIcon(typeof(GameObject))]
    public class PlayerNode : Unit
    {
        [DoNotSerialize]
        public ValueOutput OutputValue;

        /// <summary>
        /// Reference to the player
        /// </summary>
        static GameObject Player { get; set; }

        protected override void Definition()
        {
            OutputValue = ValueOutput("Player", GetPlayer);
        }

        private GameObject GetPlayer(Flow flow)
        {
            FindPlayerReference();

            if (Player == null)
            {
                Debug.Log("Player is null");
            }

            return Player;
        }

        private void FindPlayerReference()
        {
            if (Player == null)
            {
                if (Managers.TryGet<IPlayerManager>(out var playerManager))
                {
                    Player = playerManager.GetPlayer();
                }
            }
        }
    }
}