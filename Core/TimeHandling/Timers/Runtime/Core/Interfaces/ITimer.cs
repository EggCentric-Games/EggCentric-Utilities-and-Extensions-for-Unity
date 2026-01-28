using EggCentric.LifeCycleHandling;
using System;

namespace EggCentric.Timers
{
    public interface ITimer : ITickable
    {
        public event Action OnStart;
        public event Action OnCycleFinished;
        public event Action OnCompletion;
        public event Action OnInterruption;

        public void Start();
        public void Stop();
    }
}