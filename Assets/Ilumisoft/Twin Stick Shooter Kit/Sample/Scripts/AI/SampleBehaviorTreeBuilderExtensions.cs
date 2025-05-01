using Ilumisoft.AI.BehaviorTreeToolkit;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    /// <summary>
    /// A set of extension methods for the behavior tree builder. 
    /// The extension methods make it easier to create behavior trees with the builder using the custom sample nodes created for our sample agent.
    /// You can use this as an example of how you can extend the behavior tree builder with your own custom nodes.
    /// </summary>
    public static class SampleBehaviorTreeBuilderExtensions
    {
        public static BehaviorTreeBuilder StopNavigation(this BehaviorTreeBuilder builder) => builder.AddNode(new StopNavigationNode());
        public static BehaviorTreeBuilder Wander(this BehaviorTreeBuilder builder) => builder.AddNode(new WanderNode());
        public static BehaviorTreeBuilder MoveToPlayer(this BehaviorTreeBuilder builder) => builder.AddNode(new MoveToPlayerNode());
        public static BehaviorTreeBuilder OrientTowardsPlayer(this BehaviorTreeBuilder builder) => builder.AddNode(new OrientTowardsPlayerNode());
        public static BehaviorTreeBuilder SetAlerted(this BehaviorTreeBuilder builder, bool value) => builder.AddNode(new SetAlertedNode(value));
        public static BehaviorTreeBuilder IsPlayerInAttackRange(this BehaviorTreeBuilder builder) => builder.AddNode(new IsPlayerInAttackRangeNode());
        public static BehaviorTreeBuilder IsPlayerInDetectionRange(this BehaviorTreeBuilder builder) => builder.AddNode(new IsPlayerInDetectionRangeNode());
        public static BehaviorTreeBuilder IsAlerted(this BehaviorTreeBuilder builder) => builder.AddNode(new IsAlertedNode());
        public static BehaviorTreeBuilder IsDead(this BehaviorTreeBuilder builder) => builder.AddNode(new IsDead());
        public static BehaviorTreeBuilder IsPlayerAlive(this BehaviorTreeBuilder builder) => builder.AddNode(new IsPlayerAlive());
        public static BehaviorTreeBuilder FireWeapon(this BehaviorTreeBuilder builder, float duration = 1.0f) => builder.AddNode(new FireWeaponNode(duration));
    }
}