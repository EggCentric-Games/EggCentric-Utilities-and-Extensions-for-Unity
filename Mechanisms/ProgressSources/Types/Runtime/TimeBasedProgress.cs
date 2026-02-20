using EggCentric.PeriodicUpdaters;

namespace EggCentric.ProgressSources.Types
{
    public class TimeBasedProgress : IProgressSource
    {
        public float Time => _periodicUpdater.Progress + cycle;

        private readonly IPeriodicUpdater _periodicUpdater;
        private int cycle;

        public TimeBasedProgress(IPeriodicUpdater periodicUpdater)
        {
            _periodicUpdater = periodicUpdater;
            _periodicUpdater.OnUpdate += () => cycle++;
        }
    }
}