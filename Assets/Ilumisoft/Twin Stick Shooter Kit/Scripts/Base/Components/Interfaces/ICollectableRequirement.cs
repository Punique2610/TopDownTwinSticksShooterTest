using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface ICollectableRequirement
    {
        public bool IsCollectable(GameObject collector);
    }
}