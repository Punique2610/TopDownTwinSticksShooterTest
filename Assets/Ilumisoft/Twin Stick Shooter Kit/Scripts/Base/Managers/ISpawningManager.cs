using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface ISpawningManager : IComponent
    {
        GameObject Spawn(GameObject prefab, Transform parent = null);
        GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null);
        void Despawn(GameObject gameObject);
    }
}