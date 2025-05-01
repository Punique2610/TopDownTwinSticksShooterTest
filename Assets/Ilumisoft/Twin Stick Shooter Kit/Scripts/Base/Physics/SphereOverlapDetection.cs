using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public class SphereOverlapDetection : CollisionDetection
    {
        public Vector3 Position { get; set; } = Vector3.zero;

        public float Radius { get; set; } = 1.0f;

        public SphereOverlapDetection(int capacity) : base(capacity) { }

        public override List<Collider> GetColliders()
        {
            NumColliders = UnityEngine.Physics.OverlapSphereNonAlloc(Position, Radius, HitColliders, LayerMask, QueryTriggerInteraction);

            Result.Clear();

            for (int i = 0; i < NumColliders; i++)
            {
                Result.Add(HitColliders[i]);
            }

            return Result;
        }
    }
}