namespace Ilumisoft.AI.BehaviorTreeToolkit
{
    using UnityEngine;

    /// <summary>
    /// Waits for a given duration in seconds.
    /// Returns 'Success' if the duration has been passed, 'Running' otherwise.
    /// </summary>
    public class WaitNode : ActionNode
    {
        /// <summary>
        /// The time mode (scaled or unscaled)
        /// </summary>
        readonly TimeMode timeMode;

        /// <summary>
        /// The duration the node should wait
        /// </summary>
        readonly float duration = 1;

        float elapsed = 0.0f;

        /// <summary>
        /// The time when the node has been started
        /// </summary>
        float startTime;

        public WaitNode(float duration, TimeMode timeMode = TimeMode.Scaled)
        {
            this.duration = duration;
            this.timeMode = timeMode;
        }

        protected override void OnStart()
        {
            // Reset the start time
            startTime = GetTime();

            elapsed = 0.0f;
        }

        protected override StatusCode OnUpdate()
        {
            elapsed = GetElapsed();

            // Return 'Success' if the duration has been elapsed, 'Running' otherwise
            return (elapsed >= duration) ? StatusCode.Success : StatusCode.Running;
        }

        /// <summary>
        /// Get the elapsed time since the start time
        /// </summary>
        /// <returns></returns>
        float GetElapsed()
        {
            return GetTime() - startTime;
        }

        /// <summary>
        /// Gets the current time (depending on the time mode)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        float GetTime()
        {
            return timeMode switch
            {
                TimeMode.Scaled => Time.time,
                TimeMode.Unscaled => Time.unscaledTime,
                _ => throw new System.NotImplementedException()
            };
        }

        public override string ToString() => $"Wait: {elapsed:0.00}/{duration}s";
    }
}