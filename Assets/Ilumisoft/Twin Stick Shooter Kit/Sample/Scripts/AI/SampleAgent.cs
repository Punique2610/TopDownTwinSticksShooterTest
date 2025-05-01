using Ilumisoft.AI.BehaviorTreeToolkit;
using UnityEngine;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public class SampleAgent : MonoBehaviour, IBehaviorTreeComponent
    {
        [System.Serializable]
        public class Stats
        {
            [field: SerializeField]
            public float AttackRange { get; set; } = 5;

            [field: SerializeField]
            public float OrientationSpeed { get; set; } = 5;

            [field: SerializeField]
            public float DetectionRange { get; set; } = 10;
        }

        [SerializeField]
        Stats stats = new();

        IBehaviorTree behaviorTree;

        /// <summary>
        /// The defintion of the behavior tree. This is using the tree builder pattern to improve readability.
        /// </summary>
        /// <returns></returns>
        IBehaviorTree CreateBehaviorTree(GameObject owner, IBlackboard blackboard)
        {
            return new BehaviorTreeBuilder(owner, blackboard)

            .ReactiveSelector()
                // When the agent is dead, no other node will be performed
                .Sequence()
                    .IsDead()
                .End()

                .Selector()

                    // Once the player enters the detection range of the agent, it will get alerted and chase and attack the player
                    // With force failure we can execute this task without stopping the selector
                    .ForceFailure()
                        .Sequence()
                            .IsPlayerAlive()
                            .IsPlayerInDetectionRange()
                            .SetAlerted(true)
                        .End()

                    .ForceFailure()
                        .Selector()
                            .IsPlayerAlive()
                            .SetAlerted(false)
                        .End()

                    // Attack player if in attack range (highest priority)
                    .Sequence()

                        .IsAlerted()

                        // Fail if the player is not in attack range
                        .IsPlayerInAttackRange()

                        // Stop movement
                        .StopNavigation()

                        // While the agent is attacking the player it orients towards the player
                        .Parallel()
                            .OrientTowardsPlayer()

                            // The agent shoots 3 times
                            .Repeat(3)
                                .Sequence()
                                    .FireWeapon(1.0f)
                                    .Wait(0.5f)
                                .End()
                        .End()
                    .End()

                    // Chase the player if alerted
                    .Sequence()
                        .IsAlerted()
                        .MoveToPlayer()
                        .Wait(0.5f)
                    .End()

                    // Wander around randomly (lowest priority)
                    .Wander()

                .End()
            .End()
            .Build();
        }

        void Start()
        {
            // Create the blackboard and set the stats of the agent
            var blackboard = new Blackboard();
            blackboard.SetStats(stats);

            // Create our behavior tree
            behaviorTree = CreateBehaviorTree(gameObject, blackboard);
        }

        public void Tick()
        {
            // This will let the behavior tree execute its logic
            behaviorTree.Tick();
        }

        /// <summary>
        /// This is used by the behavior tree viewer to access the behavior tree
        /// </summary>
        /// <returns></returns>
        public IBehaviorTree GetBehaviorTree() => behaviorTree;
    }
}