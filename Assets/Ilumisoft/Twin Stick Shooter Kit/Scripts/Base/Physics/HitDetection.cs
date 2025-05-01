using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public class HitDetection
    {
        readonly RaycastHit[] hitBuffer;
        readonly List<RaycastHit> result;

        public LayerMask LayerMask { get; set; } = -1;

        public QueryTriggerInteraction QueryTriggerInteraction { get; set; } = QueryTriggerInteraction.Collide;

        public float Radius { get; set; } = 0.1f;

        public HitDetection(int capacity)
        {
            hitBuffer = new RaycastHit[capacity];
            result = new List<RaycastHit>(capacity);
        }

        public List<RaycastHit> DetectHit(Vector3 startPosition, Vector3 endPosition)
        {
            Vector3 movedDistance = endPosition - startPosition;

            int hitCount = Physics.SphereCastNonAlloc(startPosition, Radius, movedDistance.normalized, hitBuffer, movedDistance.magnitude, LayerMask, QueryTriggerInteraction);

            result.Clear();

            for(int i = 0; i<hitCount; i++)
            {
                result.Add(hitBuffer[i]);
            }

            return result;
        }
    }
}