using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    [System.Serializable]
    public struct HitInfo
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Collider Collider;
    }
}