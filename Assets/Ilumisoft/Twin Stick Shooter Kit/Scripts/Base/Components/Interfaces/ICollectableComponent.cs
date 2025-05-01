using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface ICollectableComponent : IComponent
    {
        void Collect(GameObject collector);
    }
}