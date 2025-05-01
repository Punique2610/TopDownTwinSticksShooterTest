using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class BehaviorTreeViewer : EditorWindow
    {
        /// <summary>
        /// The game object the viewed behavior tree belongs to
        /// </summary>
        [System.NonSerialized]
        GameObject owner = null;

        /// <summary>
        /// Label element for the owner
        /// </summary>
        Label ownerLabel;

        /// <summary>
        /// Visual element used as a container for the owner label and the tree container
        /// </summary>
        VisualElement contentContainer;

        /// <summary>
        /// Visual element used as a container holding the tree
        /// </summary>
        VisualElement treeContainer;

        /// <summary>
        /// The visual element representing the behavior tree
        /// </summary>
        BehaviorTreeVisualElement treeElement;

        /// <summary>
        /// Container for the message shown when the editor is not in playmode
        /// </summary>
        VisualElement messageBoxContainer;

        [MenuItem("Tools/Behavior Tree Toolkit/Behavior Tree Viewer")]
        public static void ShowExample()
        {
            var window = GetWindow<BehaviorTreeViewer>();
            window.titleContent = new GUIContent("Behavior Tree Viewer");
        }

        public void CreateGUI()
        {
            // Import and add the content from the UXML
            var visualTree = Resources.Load<VisualTreeAsset>("Ilumisoft/Behavior Tree Toolkit/BehaviorTreeViewer");
            VisualElement contentFromUXML = visualTree.Instantiate();
            rootVisualElement.Add(contentFromUXML);

            // Add the stylesheet
            var styleSheet = Resources.Load<StyleSheet>("Ilumisoft/Behavior Tree Toolkit/BehaviorTreeViewer");
            rootVisualElement.styleSheets.Add(styleSheet);

            // Get the label element representing the owner of the tree
            ownerLabel = rootVisualElement.Q<Label>("Owner-Label");

            // Create the conatiner for the tree
            treeContainer = new VisualElement();

            // Add the container to the scroll view. This will make the content scrollable if it gets too large for the window
            var scrollView = rootVisualElement.Q<ScrollView>();
            scrollView.Add(treeContainer);

            // Get containers
            contentContainer = rootVisualElement.Q<VisualElement>("content-container");
            messageBoxContainer = rootVisualElement.Q<VisualElement>("message-box-container");

            var messageBox = new HelpBox("Please start playmode and select the game object containing the behavior tree.", HelpBoxMessageType.Info);
            messageBoxContainer.Add(messageBox);
        }

        private void Update()
        {
            // Since the behavior tree is only build at runtime, it can only be displayed at runtime
            if (!Application.isPlaying)
            {
                if (messageBoxContainer != null && contentContainer != null)
                {
                    messageBoxContainer.style.display = DisplayStyle.Flex;
                    contentContainer.style.display = DisplayStyle.None;
                }

                return;
            }

            // Hide the message box
            messageBoxContainer.style.display = DisplayStyle.None;

            // Show the tree container content
            contentContainer.style.display = DisplayStyle.Flex;

            // Has  no object been selected or the active one destroyed?
            // Try to find a behavior tree
            if (owner == null)
            {
                treeContainer.Clear();
                FindBehaviorTree();
            }

            // Has the selection changed?
            if (owner != Selection.activeGameObject)
            {
                FindBehaviorTree();
            }

            // Update the tree
            if (owner != null)
            {
                treeElement.Update();
            }
        }

        /// <summary>
        /// Tries to find a behavior tree component on the selected game object
        /// </summary>
        void FindBehaviorTree()
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }

            var gameObject = Selection.activeGameObject;

            if (gameObject.TryGetComponent<IBehaviorTreeComponent>(out var behaviorTreeComponent))
            {
                SetOwner(gameObject);
                SetBehaviorTree(behaviorTreeComponent.GetBehaviorTree());
            }
        }

        /// <summary>
        /// Sets the given game object as the owner
        /// </summary>
        /// <param name="gameObject"></param>
        void SetOwner(GameObject gameObject)
        {
            owner = gameObject;
            ownerLabel.text = owner.name;
        }

        /// <summary>
        /// Sets the behavior tree that should be visualized
        /// </summary>
        /// <param name="behaviorTree"></param>
        void SetBehaviorTree(IBehaviorTree behaviorTree)
        {
            var behaviorTreeInfo = new BehaviorTreeInfo(behaviorTree);

            treeContainer.Clear();
            treeElement = new BehaviorTreeVisualElement(behaviorTreeInfo);
            treeContainer.Add(treeElement);
        }
    }
}