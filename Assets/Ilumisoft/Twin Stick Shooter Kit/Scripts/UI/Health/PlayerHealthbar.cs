using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.UI
{
    [DefaultExecutionOrder(10)]
    public class PlayerHealthbar : Healthbar
    {
        GameObject Player { get; set; }

        protected override void Awake()
        {
            base.Awake();

            if (Managers.TryGet<IPlayerManager>(out var playerManager))
            {
                Player = playerManager.GetPlayer();

                Health = Player.GetComponent<IHealthComponent>();
            }
        }
    }
}