using EggCentric.LifeCycleHandling;
using System;

namespace EggCentric.PeriodicUpdaters
{
    public interface IPeriodicUpdater : ITickable
    {
        public float RemainingTime { get; }
        public float Progress { get; }

        public event Action OnUpdate;

        public void WithPeriod(float updatePeriod);
        public void Reset();
    }
}
