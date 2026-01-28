namespace EggCentric.LifeCycleHandling
{
    public class TurnBasedTicking : ITickingStrategy
    {
        public float GetDelta(float _) => 1;
    }
}
