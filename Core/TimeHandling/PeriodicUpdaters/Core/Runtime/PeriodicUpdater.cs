using EggCentric.LifeCycleHandling;
using System;
using UnityEngine;

namespace EggCentric.PeriodicUpdaters
{
    public class PeriodicUpdater : IPeriodicUpdater
    {
        public float RemainingTime => _remainingTime;
        public float Progress => 1 - (_remainingTime / _currentPeriod);

        private IUpdatePeriodProvider _updatePeriodProvider;
        private ITickingStrategy _tickingStrategy;

        private float _currentPeriod;
        private float _remainingTime;

        public event Action OnUpdate;

        public PeriodicUpdater(IUpdatePeriodProvider updatePeriodProvider, ITickingStrategy tickingStrategy)
        {
            _updatePeriodProvider = updatePeriodProvider;
            _tickingStrategy = tickingStrategy;
        }

        public void WithPeriod(float updatePeriod) => _updatePeriodProvider = new FixedPeriodProvider(updatePeriod);

        public void Tick(float timeStep)
        {
            _remainingTime -= _tickingStrategy.GetDelta(timeStep);

            while (_remainingTime <= 0)
            {
                if(!UpdatePeriod())
                {
                    Debug.LogError($"Update period must be greater than zero. CurrentPeriod is {_currentPeriod}. Diverging.");
                    break;
                }

                _remainingTime += _currentPeriod; 
                OnUpdate?.Invoke();
            }
        }

        public void Reset()
        {
            _updatePeriodProvider.Reset();
            UpdatePeriod();
            _remainingTime = _currentPeriod;
        }

        private bool UpdatePeriod()
        {
            _currentPeriod = _updatePeriodProvider.GetNext();
            if(_currentPeriod > 0)
                return true;

            return false;
        }
    }
}
