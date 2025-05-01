using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Minimap Manager
    /// </summary>
    public class MinimapManager : ManagerComponent, IMinimapManager
    {
        public List<GameObject> Elements { get; set; } = new();

        public UnityAction<GameObject> OnElementAdded { get; set; }

        public UnityAction<GameObject> OnElementRemoved { get; set; }

        public void Register(GameObject element)
        {
            if (Elements.Contains(element) == false)
            {
                Elements.Add(element);

                OnElementAdded?.Invoke(element);
            }
        }

        public void Unregister(GameObject element)
        {
            if (Elements.Contains(element))
            {
                Elements.Remove(element);

                OnElementRemoved?.Invoke(element);
            }
        }
    }
}