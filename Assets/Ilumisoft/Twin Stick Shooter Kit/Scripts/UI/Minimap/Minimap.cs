using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit
{
    [DefaultExecutionOrder(-10)]
    public class Minimap : MonoBehaviour
    {
        readonly Dictionary<IMinimapElementComponent, IMinimapMarkerComponent> markerDictionary = new();

        [SerializeField]
        private RectTransform elementSocket;

        [SerializeField]
        private float range = 20;

        public float Range { get => range; set => range = value; }

        IMinimapManager MinimapManager { get; set; }

        IPlayerManager PlayerManager { get; set; }

        GameObject Player { get; set; }

        private void Awake()
        {
            MinimapManager = Managers.Get<IMinimapManager>();
            PlayerManager = Managers.Get<IPlayerManager>();
        }

        private void OnEnable()
        {
            if (!MinimapManager.IsNullOrDestroyed())
            {
                MinimapManager.OnElementAdded += OnElementAdded;
                MinimapManager.OnElementRemoved += OnElementRemoved;
            }
        }

        private void OnDisable()
        {
            if (!MinimapManager.IsNullOrDestroyed())
            {
                MinimapManager.OnElementAdded -= OnElementAdded;
                MinimapManager.OnElementRemoved -= OnElementRemoved;
            }
        }
        private void OnElementAdded(GameObject element)
        {
            if(element.TryGetComponent<IMinimapElementComponent>(out var minimapElement) && !markerDictionary.ContainsKey(minimapElement))
            {
                var marker = minimapElement.CreateMarker();

                marker.GameObject.transform.SetParent(elementSocket.transform, false);

                markerDictionary.Add(minimapElement, marker);
            }
        }

        private void OnElementRemoved(GameObject element)
        {
            if (element.TryGetComponent<IMinimapElementComponent>(out var minimapElement))
            {
                if (markerDictionary.TryGetValue(minimapElement, out IMinimapMarkerComponent marker))
                {
                    markerDictionary.Remove(minimapElement);

                    Destroy(marker.GameObject);
                }
            }
        }

        private void Update()
        {
            if (PlayerManager.TryGetPlayer(out var player))
            {
                Player = player;

                UpdateMinimapElements();
            }
        }

        private void UpdateMinimapElements()
        {
            foreach (var minimapElement in markerDictionary.Keys)
            {
                if (markerDictionary.TryGetValue(minimapElement, out var marker))
                {
                    if (TryGetMinimapLocation(minimapElement, out var minimapLocation))
                    {
                        marker.SetVisible(true);

                        var rectTransform = marker.GetComponent<RectTransform>();

                        rectTransform.anchoredPosition = minimapLocation;
                    }
                    else
                    {
                        marker.SetVisible(false);
                    }
                }
            }
        }

        private bool TryGetMinimapLocation(IMinimapElementComponent minimapElement, out Vector2 minimapLocation)
        {
            minimapLocation = GetDistanceToPlayer(minimapElement);

            float minimapSize = GetMinimapSize();

            var scale = minimapSize / Range;

            minimapLocation *= scale;

            if (minimapLocation.magnitude < minimapSize || minimapElement.AlwaysVisible)
            {
                // Make sure it is not shown outside the minimap
                minimapLocation = Vector2.ClampMagnitude(minimapLocation, minimapSize);

                return true;
            }

            return false;
        }

        private float GetMinimapSize()
        {
            return elementSocket.rect.width / 2;
        }

        /// <summary>
        /// Returns the distance to the player on the x,z plane
        /// </summary>
        /// <param name="minimapElement"></param>
        /// <returns></returns>
        private Vector2 GetDistanceToPlayer(IMinimapElementComponent minimapElement)
        {
            Vector3 distanceToPlayer = minimapElement.GameObject.transform.position - Player.transform.position;

            return new Vector2(distanceToPlayer.x, distanceToPlayer.z);
        }
    }
}