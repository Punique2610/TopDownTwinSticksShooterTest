using System.Collections.Generic;
using UnityEngine.UIElements;

namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    public class BehaviorTreeVisualElement : VisualElement
    {
        /// <summary>
        /// List of all node elements, representing the nodes of the tree
        /// </summary>
        readonly List<NodeVisualElement> nodeElements = new();

        public BehaviorTreeVisualElement(IBehaviorTreeInfo behaviorTreeInfo)
        {
            if (behaviorTreeInfo != null)
            {
                CreateNodeElementList(behaviorTreeInfo);
            }
        }

        /// <summary>
        /// Creates a list of node elements, which represent the nodes of the tree
        /// </summary>
        /// <param name="behaviorTreeInfo"></param>
        void CreateNodeElementList(IBehaviorTreeInfo behaviorTreeInfo)
        {
            // Remove all existing elements added to this element
            this.Clear();

            foreach(var nodeInfo in behaviorTreeInfo.Nodes)
            {
                var nodeElement = new NodeVisualElement(nodeInfo);

                // Add the element to the list so that it can be updated later on
                nodeElements.Add(nodeElement);

                // Add the element to the content container of the visual element
                this.Add(nodeElement);
            }
        }

        /// <summary>
        /// Update all node elements of the tree
        /// </summary>
        public void Update()
        {
            foreach (var nodeElement in nodeElements)
            {
                nodeElement.Update();
            }

            int depth = int.MaxValue;

            // Hide non expanded nodes and all of their children
            for(int i=0; i<nodeElements.Count; i++)
            {
                var element = nodeElements[i];

                if(element.DepthLevel<depth && element.IsExpanded == false)
                {
                    depth = element.DepthLevel;
                }

                if (element.DepthLevel == depth && element.IsExpanded)
                {
                    depth = int.MaxValue;
                }

                if (element.DepthLevel>depth)
                {
                    element.SetHidden(true);
                }
                else
                {
                    element.SetHidden(false);
                }
            }
        }
    }
}