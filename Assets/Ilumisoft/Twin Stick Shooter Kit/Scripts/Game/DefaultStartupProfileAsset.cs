using System.Collections.Generic;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Game
{
    /// <summary>
    /// Default implementation of the Startup Profile Asset.
    /// The startup profile is executed automatically at startup and creates all the persistent systems of the game.
    /// You can create your own custom startup profiles and set them in the project settings.
    /// </summary>
    [CreateAssetMenu(menuName = "Configuration/Startup Profile", fileName = "Startup Profile")]
    public class DefaultStartupProfileAsset : StartupProfileAsset
    {
        public List<ManagerComponent> ManagerPrefabs;

        [Tooltip("All prefabs in this list will automatically be instantiated on start up and marked as DontDestroyOnLoad")]
        public List<GameObject> PersistentObjectPrefabs;

        public override void Initialize()
        {
            InitializeMessageSystem();
            InitializeManagerSystem();
            InitializePersistentObjects();
        }

        void InitializeMessageSystem()
        {
            // Create a persistent message broadcaster
            var messageBroadcaster = new GameObject("Message Broker").AddComponent<DefaultMessageBroker>();

            DontDestroyOnLoad(messageBroadcaster.gameObject);

            // Inject the message broadcaster into the Messages class using property injection
            Messages.MessageBroker = messageBroadcaster;
        }

        void InitializeManagerSystem()
        {
            // Create a persistent manager provider
            var managerProvider = new GameObject("Managers").AddComponent<DefaultManagerProvider>();

            DontDestroyOnLoad(managerProvider.gameObject);

            // Inject the manager provider into the static Managers class using property injection
            Managers.ManagerProvider = managerProvider;

            List<ManagerComponent> managers = new();

            // Create Managers
            foreach (var managerPrefab in ManagerPrefabs)
            {
                if (managerPrefab != null)
                {
                    var manager = Instantiate(managerPrefab);

                    manager.name = managerPrefab.name;

                    Managers.Register(manager);

                    managers.Add(manager);
                }
            }

            // Initialize managers
            foreach (var manager in managers)
            {
                manager.OnInitialize();
            }
        }

        void InitializePersistentObjects()
        {
            foreach (var prefab in PersistentObjectPrefabs)
            {
                var instance = Instantiate(prefab);

                instance.name = prefab.name;

                DontDestroyOnLoad(instance);
            }
        }
    }
}