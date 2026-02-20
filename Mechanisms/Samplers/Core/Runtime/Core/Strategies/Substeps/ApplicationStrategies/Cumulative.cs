namespace EggCentric.Samplers
{
    public class Cumulative : IApplicationStrategy
    {
        private readonly IBorderStrategy _borderStrategy;

        public Cumulative(IBorderStrategy borderStrategy) => _borderStrategy = borderStrategy;

        public float Apply(float time, float _) => _borderStrategy.Process(time, 1);
    }
}