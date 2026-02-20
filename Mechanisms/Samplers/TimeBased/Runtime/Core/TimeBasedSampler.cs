using EggCentric.ValueProviders.DataContainers;
using EggCentric.PeriodicUpdaters;
using EggCentric.ProgressSources.Types;

namespace EggCentric.Samplers.TimeBased
{
    public class TimeBasedSampler : Sampler, ITimeBasedSampler
    {
        public Field<float> Speed { get; }

        private readonly IPeriodicUpdater _periodicUpdater;

        public TimeBasedSampler(IPeriodicUpdater periodicUpdater, SamplingStrategy strategy, float speed = 1f, float step = 0f) : base(new TimeBasedProgress(periodicUpdater) , strategy, step)
        {
            _periodicUpdater = periodicUpdater;

            Speed = new Field<float>(speed);
            Speed.OnValueChanged += SetUpdatePeriod;
        }

        public void Tick(float timeStep) => _periodicUpdater.Tick(timeStep);

        private void SetUpdatePeriod(float speed) => _periodicUpdater.WithPeriod(1f / speed);
    }
}