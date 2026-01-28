using EggCentric.LifeCycleHandling;
using System;
using UnityEngine;

namespace EggCentric.PeriodicUpdaters
{
    public class PeriodicUpdater : IPeriodicUpdater
    {
        private IUpdatePeriodProvider _updatePeriodProvider;
        private ITickingStrategy _tickingStrategy;
        private float _remainingPeriod;

        public event Action OnUpdate;

        public PeriodicUpdater(IUpdatePeriodProvider updatePeriodProvider, ITickingStrategy tickingStrategy)
        {
            _updatePeriodProvider = updatePeriodProvider;
            _tickingStrategy = tickingStrategy;
        }

        public void Tick(float timeStep)
        {
            _remainingPeriod -= _tickingStrategy.GetDelta(timeStep);

            while (_remainingPeriod <= 0)
            {
                var next = _updatePeriodProvider.GetNext();
                if(next <= 0)
                {
                    Debug.LogError($"Update period must be greater than zero. CurrentPeriod is {next}. Diverging.");
                    break;
                }

                _remainingPeriod += next; 
                OnUpdate?.Invoke();
            }
        }

        public void Reset()
        {
            _updatePeriodProvider.Reset();
            _remainingPeriod = _updatePeriodProvider.GetNext();
        }
    }
}
