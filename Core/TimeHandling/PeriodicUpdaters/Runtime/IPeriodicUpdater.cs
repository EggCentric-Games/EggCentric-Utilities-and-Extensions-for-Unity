using EggCentric.LifeCycleHandling;
using System;

namespace EggCentric.PeriodicUpdaters
{
    public interface IPeriodicUpdater : ITickable
    {
        public event Action OnUpdate;

        public void Reset();
    }
}
