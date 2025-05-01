using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IPlayerManager
    {
        GameObject GetPlayer();
        bool TryGetPlayer(out GameObject player);
    }
}