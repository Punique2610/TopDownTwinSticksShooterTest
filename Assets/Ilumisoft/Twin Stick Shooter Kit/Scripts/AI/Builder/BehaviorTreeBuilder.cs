namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;

    /// <summary>
    /// The BehaviorTreeBuilder provides a convenient and more readable way to create behavior trees from code by using method chaining.
    /// If you want to add new methods/nodes to the builder we recommend to use extension methods. You can also simply call the AddNode method
    /// </summary>
    public class BehaviorTreeBuilder
    {
        readonly GameObject owner;
        readonly IBlackboard blackboard;
        INode rootNode = null;

        /// <summary>
        /// Since composites and decorators can have child nodes, we need to manage to which parent node a new node will be added.
        /// The branch stack allows us to do exactly this, while the top element is always the current branch new nodes will be added to.
        /// </summary>
        readonly Stack<Node> branchStack = new();

        public BehaviorTreeBuilder(GameObject owner, IBlackboard blackboard)
        {
            if(owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            if(blackboard == null)
            {
                throw new ArgumentNullException(nameof(blackboard));
            }

            this.owner = owner;
            this.blackboard = blackboard;
        }

        /// <summary>
        /// Returns the behavior tree instance created by the builder
        /// </summary>
        /// <returns></returns>
        public IBehaviorTree Build()
        {
            return new BehaviorTree(owner, blackboard, rootNode);
        }

        /// <summary>
        /// Adds the given node to the behavior tree. 
        /// If the given node is a composite node (like selector, sequence, parallel), new nodes will become it's children until CloseBranch() is called.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public BehaviorTreeBuilder AddNode(Node node)
        {
            // Is stack empty?
            if (branchStack.Count == 0)
            {
                // Added node is the root node
                rootNode = node;
            }
            else
            {
                var branch = branchStack.Pop();

                AddChildToBranch(branch, node);

                // If the branch is a composite node, we need to push it back on the stack,
                // since it can get more than a single child attached to it.
                // Since decorators can only have a single child, we can consider a decorator branch to be closed once a child has been addded.
                if (branch is ICompositeNode)
                {
                    branchStack.Push(branch);
                }
            }

            // If the added node is not a leaf node, it becomes the new active branch
            if (node is IDecoratorNode || node is ICompositeNode)
            {
                branchStack.Push(node);
            }

            return this;
        }

        /// <summary>
        /// Adds the given child node to the given branch
        /// </summary>
        /// <param name="branch"></param>
        /// <param name="child"></param>
        void AddChildToBranch(Node branch, Node child)
        {
            if (branch is IDecoratorNode decoratorNode)
            {
                decoratorNode.Child = child;
            }

            if (branch is ICompositeNode compositeNode)
            {
                compositeNode.AddChild(child);
            }
        }

        /// <summary>
        /// Closes the currently open branch. 
        /// </summary>
        /// <returns></returns>
        public BehaviorTreeBuilder End()
        {
            branchStack.Pop();

            return this;
        }

        #region Composites
        public BehaviorTreeBuilder Parallel(AbortPolicy abortPolicy = AbortPolicy.Running) => AddNode(new ParallelNode(abortPolicy));
        public BehaviorTreeBuilder ReactiveSequence(AbortPolicy abortPolicy = AbortPolicy.Running) => AddNode(new ReactiveSequenceNode(abortPolicy));
        public BehaviorTreeBuilder ReactiveSelector(AbortPolicy abortPolicy = AbortPolicy.Running) => AddNode(new ReactiveSelectorNode(abortPolicy));
        public BehaviorTreeBuilder Selector() => AddNode(new SelectorNode());
        public BehaviorTreeBuilder Sequence() => AddNode(new SequenceNode());
        #endregion

        #region Decorators
        public BehaviorTreeBuilder ForceFailure() => AddNode(new ForceFailureNode());
        public BehaviorTreeBuilder ForceSuccess() => AddNode(new ForceSuccessNode());
        public BehaviorTreeBuilder Invert() => AddNode(new InvertNode());
        public BehaviorTreeBuilder Repeat(int count) => AddNode(new RepeatNode(count));
        public BehaviorTreeBuilder Retry(int count) => AddNode(new RetryNode(count));
        #endregion

        #region Actions
        public BehaviorTreeBuilder Action(string name, UnityAction action) => AddNode(new InvokeAction(name, action));
        public BehaviorTreeBuilder Log(string message) => AddNode(new DebugLogNode(message));
        public BehaviorTreeBuilder SetActive(GameObject gameObject, Func<bool> value) => AddNode(new SetActiveNode(gameObject, value));
        public BehaviorTreeBuilder SetActive(GameObject gameObject, bool value) => AddNode(new SetActiveNode(gameObject, () => value));
        public BehaviorTreeBuilder SetBlackboardEntry<T>(string key, T value) => AddNode(new SetBlackboardEntryNode<T>(key, value));
        public BehaviorTreeBuilder SetBlackboardEntry<T>(string key, Func<T> value) => AddNode(new SetBlackboardEntryNode<T>(key, value));
        public BehaviorTreeBuilder SubTree(IBehaviorTree behaviorTree) => AddNode(new SubTreeNode(behaviorTree));
        public BehaviorTreeBuilder Wait(float duration, TimeMode timeMode = TimeMode.Scaled) => AddNode(new WaitNode(duration, timeMode));
        #endregion

        #region Conditions
        public BehaviorTreeBuilder Condition(string name, Func<bool> condition) => AddNode(new IsConditionMetNode(name, condition));
        public BehaviorTreeBuilder ContainsBlackboardEntry<T>(string key) => AddNode(new ContainsBlackboardEntry<T>(key));
        public BehaviorTreeBuilder ContainsBlackboardEntry(string key) => AddNode(new ContainsBlackboardEntry(key));
        public BehaviorTreeBuilder CompareBlackboardEntry<T>(string key, T value) => AddNode(new CompareBlackboardEntry<T>(key, value));
        #endregion 
    }
}