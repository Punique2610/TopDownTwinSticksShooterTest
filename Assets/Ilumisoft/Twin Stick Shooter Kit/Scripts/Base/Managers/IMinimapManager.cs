using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Ilumisoft.TwinStickShooterKit
{
    public interface IMinimapManager : IComponent
    {
        List<GameObject> Elements { get; set; }
        UnityAction<GameObject> OnElementAdded { get; set; }
        UnityAction<GameObject> OnElementRemoved { get; set; }

        void Register(GameObject minimapElement);
        void Unregister(GameObject minimapElement);
    }
}