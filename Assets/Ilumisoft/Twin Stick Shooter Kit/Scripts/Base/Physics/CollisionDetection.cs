using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public abstract class CollisionDetection
    {
        protected Collider[] HitColliders { get; set; }

        protected int NumColliders { get; set; } = 0;

        public LayerMask LayerMask { get; set; } = 0;

        public QueryTriggerInteraction QueryTriggerInteraction { get; set; } = QueryTriggerInteraction.UseGlobal;

        public abstract List<Collider> GetColliders();

        protected List<Collider> Result { get; set; }

        public CollisionDetection(int capacity)
        {
            HitColliders = new Collider[capacity];
            Result = new List<Collider>(capacity);
        }
    }
}