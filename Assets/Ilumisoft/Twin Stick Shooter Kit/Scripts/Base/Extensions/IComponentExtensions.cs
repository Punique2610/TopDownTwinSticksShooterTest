using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    public static class IComponentExtensions
    {
        public static bool IsNullOrDestroyed(this IComponent component)
        {
            // If the given object is a UnityEngine.Object use the overriden == operator
            if (component is Object unityObject)
            {
                return unityObject == null;
            }
            // Otherwise use the default == operator
            else
            {
                return component == null;
            }
        }

        public static bool TryGetComponent<T>(this IComponent comp, out T component)
        {
            return comp.GameObject.TryGetComponent(out component);
        }

        public static T GetComponent<T>(this IComponent component)
        {
            return component.GameObject.GetComponent<T>();
        }
    }
}