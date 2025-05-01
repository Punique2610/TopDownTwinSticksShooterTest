using UnityEngine.UIElements;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class NodeVisualElement : VisualElement
    {
        /// <summary>
        /// All class names used by this element
        /// </summary>
        public class ClassNames
        {
            public const string Node = "behavior-tree-node";
            public const string NodeComposite = "behavior-tree-node-composite";
            public const string NodeDecorator = "behavior-tree-node-decorator";
            public const string NodeCondition = "behavior-tree-node-condition";
            public const string NodeAction = "behavior-tree-node-action";

            public const string NodeStateIcon = "behavior-tree-state-icon";
            public const string NodeStateIconCondition = "behavior-tree-state-icon-condition";
            public const string NodeStateRunning = "behavior-tree-state-running";
            public const string NodeStateFailure = "behavior-tree-state-failure";
            public const string NodeStateSuccess = "behavior-tree-state-success";
            public const string NodeStateAborted = "behavior-tree-state-aborted";

        }

        /// <summary>
        /// Reference to the node info
        /// </summary>
        readonly INodeInfo nodeInfo;

        /// <summary>
        /// Visual element representing the icon element of the node
        /// </summary>
        readonly VisualElement iconElement;

        /// <summary>
        /// Visual element representing the label element of the node
        /// </summary>
        readonly VisualElement labelElement;

        public bool IsExpanded
        {
            get 
            {
                if (labelElement is Foldout foldout)
                {
                   return foldout.value;
                }
                else
                {
                    return true;
                }
            }
        }

        public int DepthLevel { get; private set; }
        
        public void SetHidden(bool hidden)
        {
            if(hidden)
            {
                style.display = DisplayStyle.None;
            }
            else
            {
                style.display = DisplayStyle.Flex;
            }
        }

        public NodeVisualElement(INodeInfo nodeInfo) : base()
        {
            this.nodeInfo = nodeInfo;

            // Add the style class and adjust the margin using the depth level.
            // This will make each node element being properly indented.
            this.AddToClassList(ClassNames.Node);
            this.style.marginLeft = nodeInfo.DepthLevel * 16;
            DepthLevel = nodeInfo.DepthLevel;

            // Create the icon and label element and add them to this element
            iconElement = new VisualElement();

            if (nodeInfo.Type == NodeType.Composite || nodeInfo.Type == NodeType.Decorator)
            {
                labelElement = new Foldout();
            }
            else
            {
                labelElement = new Label();
            }


            labelElement.focusable = false;

            Add(iconElement);
            Add(labelElement);
        }

        /// <summary>
        /// Updates the appearance of the element to match the current node state
        /// </summary>
        public void Update()
        {
            UpdateLabel();
            UpdateClassList();
        }

        /// <summary>
        /// Updates the label of the element
        /// </summary>
        void UpdateLabel()
        {
            if (labelElement is Label label)
            {
                label.text = nodeInfo.ToString();
            }
            else if(labelElement is Foldout foldout)
            {
                foldout.text = nodeInfo.ToString();
            }
        }

        /// <summary>
        /// Updates the class list (stylesheet) depending on the node state.
        /// </summary>
        void UpdateClassList()
        {
            // Remove all existing style classes
            labelElement.ClearClassList();
            iconElement.ClearClassList();

            // Depending on the type of node, the label will look different
            switch (nodeInfo.Type)
            {
                case NodeType.Composite:
                    labelElement.AddToClassList(ClassNames.NodeComposite);

                    break;
                case NodeType.Decorator:
                    labelElement.AddToClassList(ClassNames.NodeDecorator);
                    break;
                case NodeType.Condition:
                    labelElement.AddToClassList(ClassNames.NodeCondition);

                    break;
                case NodeType.Action:
                    labelElement.AddToClassList(ClassNames.NodeAction);

                    break;
            }

            // Depending on the type of node, the icon will look different
            switch (nodeInfo.Type)
            {
                case NodeType.Composite:
                case NodeType.Decorator:
                case NodeType.Action:
                    iconElement.AddToClassList(ClassNames.NodeStateIcon);
                    break;
                case NodeType.Condition:
                    iconElement.AddToClassList(ClassNames.NodeStateIconCondition);
                    break;
            }

            // Depending on the node state (none, success, running, failure, aborted),
            // the appearance of the icon should be changed
            switch (nodeInfo.State)
            {
                case NodeState.Failure:
                    iconElement.AddToClassList(ClassNames.NodeStateFailure);
                    break;
                case NodeState.Running:
                    iconElement.AddToClassList(ClassNames.NodeStateRunning);
                    break;
                case NodeState.Success:
                    iconElement.AddToClassList(ClassNames.NodeStateSuccess);
                    break;
                case NodeState.Aborted:
                    iconElement.AddToClassList(ClassNames.NodeStateAborted);
                    break;
            }
        }
    }
}