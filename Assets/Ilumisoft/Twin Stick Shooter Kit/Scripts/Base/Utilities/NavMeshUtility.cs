using UnityEngine;
using UnityEngine.AI;

namespace Ilumisoft.TwinStickShooterKit.Utilities
{
    public static class NavMeshUtility
    {
        public static Vector3 GetRandomPoint(Vector3 center, float radius)
        {
            // If the radius is <=0, sample the given center position
            if (Mathf.Approximately(radius, 0.0f) || radius<0)
            {
                return GetRandomPointInternal(center, 0.0f);
            }
            else
            {
                Vector3 result = center;

                int steps = Mathf.CeilToInt(radius);

                float stepRadius = radius / steps;

                for (int i = 0; i < steps; i++)
                {
                    result = GetRandomPointInternal(result, stepRadius);
                }

                return result;
            }
        }

        static Vector3 GetRandomPointInternal(Vector3 center, float radius)
        {
            for (int i = 0; i < 10; i++)
            {
                var randomPoint = center + Random.insideUnitSphere * radius;

                if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas))
                {
                    return hit.position;
                }
            }

            return center;
        }
    }
}