namespace EggCentric.Samplers
{
    public class SamplingStrategy
    {
        public float Step { get; set; }

        private readonly IBorderStrategy _borderStrategy;
        private readonly ITimeProcessor _timeProcessor;
        private readonly IApplicationStrategy _applicationStrategy;

        public SamplingStrategy(IBorderStrategy borderStrategy, ITimeProcessor timeProcessor, IApplicationStrategy applicationStrategy)
        {
            _borderStrategy = borderStrategy;
            _timeProcessor = timeProcessor;
            _applicationStrategy = applicationStrategy;
        }

        public float GetSample(float time)
        {
            if (Step <= 0)
                return _borderStrategy.Process(time, 1);

            var convertedTime = _timeProcessor.Process(time, Step);
            var result = _applicationStrategy.Apply(convertedTime, Step);

            return  result;
        }
    }
}