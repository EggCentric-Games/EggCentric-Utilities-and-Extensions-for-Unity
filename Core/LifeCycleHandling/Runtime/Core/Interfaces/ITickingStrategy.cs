namespace EggCentric.LifeCycleHandling
{
    public interface ITickingStrategy
    {
        public float GetDelta(float timeStep);
    }
}
