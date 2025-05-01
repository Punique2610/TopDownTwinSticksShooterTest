using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IPool
    {
        bool IsEmpty { get; }

        void Add(GameObject gameObject);
        GameObject Get();
    }
}