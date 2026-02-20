namespace EggCentric.Samplers
{
    public class Repetative : IApplicationStrategy
    {
        private readonly IBorderStrategy _borderStrategy;
        private readonly ISegmentationStrategy _segmentationStrategy;

        public Repetative(IBorderStrategy borderStrategy, ISegmentationStrategy segmentationStrategy)
        {
            _borderStrategy = borderStrategy;
            _segmentationStrategy = segmentationStrategy;
        }

        public float Apply(float time, float step)
        {
            var limit = _segmentationStrategy.Process(step);
            var result = _borderStrategy.Process(time, limit) * step;

            return result;
        }
    }
}