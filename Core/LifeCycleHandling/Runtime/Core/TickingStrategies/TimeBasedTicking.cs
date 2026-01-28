namespace EggCentric.LifeCycleHandling
{
    public class TimeBasedTicking : ITickingStrategy
    {
        public float GetDelta(float timeStep) => timeStep;
    }
}
