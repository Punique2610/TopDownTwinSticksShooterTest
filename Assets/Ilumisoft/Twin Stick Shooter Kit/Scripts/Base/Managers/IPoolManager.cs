using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IPoolManager
    {
        IPool GetPool(GameObject prefab);
    }
}