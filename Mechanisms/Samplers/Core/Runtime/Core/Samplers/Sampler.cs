using EggCentric.ValueProviders.DataContainers;
using EggCentric.ProgressSources;

namespace EggCentric.Samplers
{
    public class Sampler : ISampler
    {
        public Field<float> Step { get; }

        private readonly IProgressSource _progressSource;
        private SamplingStrategy _samplingStrategy;

        public Sampler(IProgressSource progressSource, SamplingStrategy strategy, float step = 0f)
        {
            _progressSource = progressSource;

            Step = new Field<float>(step);
            Step.OnValueChanged += ChangeStrategyStep;

            WithStrategy(strategy);
        }

        public Sampler WithStrategy(SamplingStrategy samplingStrategy)
        {
            _samplingStrategy = samplingStrategy;
            ChangeStrategyStep(Step.Value);
            return this;
        }

        public float GetSample() => _samplingStrategy.GetSample(_progressSource.Time);

        private void ChangeStrategyStep(float step) => _samplingStrategy.Step = step;
    }
}