namespace Ilumisoft.TwinStickShooterKit
{
    public class Timer
    {
        public float Duration { get; set; }

        public float ElapsedTime { get; set; }

        public bool IsElapsed = false;

        public void Restart()
        {
            Start(Duration);
        }

        public void Start(float duration)
        {
            this.Duration = duration;
            ElapsedTime = 0.0f;
            IsElapsed = false;
        }

        public void Tick(float deltaTime)
        {
            if(IsElapsed)
            {
                return;
            }

            ElapsedTime += deltaTime;

            if(ElapsedTime >= Duration)
            {
                ElapsedTime = Duration;

                IsElapsed = true;
            }
        }
    }
}