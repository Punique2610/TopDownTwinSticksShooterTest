using Ilumisoft.AI.BehaviorTreeToolkit;

namespace Ilumisoft.TwinStickShooterKit.Sample
{
    public static class SampleBlackboardExtensions
    {
        static class Key
        {
            public const string IsAlerted = "Is Alerted";
            public const string SampleStats = "Sample Agent Stats";
        }

        public static bool IsAlerted(this IBlackboard blackboard) => blackboard.Get(Key.IsAlerted, false);
        public static void SetAlerted(this IBlackboard blackboard, bool value) => blackboard.Set(Key.IsAlerted, value);
        public static SampleAgent.Stats GetStats(this IBlackboard blackboard) => blackboard.Get<SampleAgent.Stats>(Key.SampleStats, null);
        public static void SetStats(this IBlackboard blackboard, SampleAgent.Stats stats) => blackboard.Set(Key.SampleStats, stats);
    }
}