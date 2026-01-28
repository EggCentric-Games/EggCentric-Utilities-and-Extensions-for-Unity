using EggCentric.LifeCycleHandling;
using EggCentric.PeriodicUpdaters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Timers
{
    public abstract class Timer : IPausableTimer
    {
        public bool IsPaused { get; private set; }

        private IPeriodicUpdater _updater;
        private IResetStrategy _resetStrategy;

        private int _currentCycle;
        private bool _isActive;

        public Timer(float period, bool isTurnBased = false) : this(new FixedPeriodProvider(period), isTurnBased ? new TurnBasedTicking() : new TimeBasedTicking()) { }
        public Timer(IEnumerable<float> sequence, bool isTurnBased = false) : this(new SequentialUpdatePeriodProvider(sequence), isTurnBased ? new TurnBasedTicking() : new TimeBasedTicking()) { }
        public Timer(IUpdatePeriodProvider updatePeriodProvider, ITickingStrategy tickingStrategy) : this(new PeriodicUpdater(updatePeriodProvider, tickingStrategy)) { }
        public Timer(IPeriodicUpdater updater)
        {
            if(updater == null)
            {
                Debug.LogError($"Provided updater is invalid. Updater must not be null. Rejecting.");
                return;
            }

            _updater = updater;
            _updater.OnUpdate += HandleCycleEnd;
        }

        public event Action OnStart;
        public event Action OnCycleFinished;
        public event Action OnCompletion;
        public event Action OnInterruption;
        public event Action OnPause;
        public event Action OnResume;

        public void Once()
        {
            Reset();
            _resetStrategy = ResetStrategies.Once;
            Start();
        }

        public void Repeat(int count)
        {
            if(count <= 0)
            {
                Debug.LogError($"Repeat count shoud be greater than zero. Using single shout instead");
                Once();
                return;
            }

            Reset();
            _resetStrategy = new RepeatingStrategy(new Getter<int>(() => _currentCycle), count);
            Start();
        }

        public void Endless()
        {
            Reset();
            _resetStrategy = ResetStrategies.Endless;
            Start();
        }

        public void Start()
        {
            if(_isActive)
                return;

            _isActive = true;
            IsPaused = false;
            OnStart?.Invoke();
        }

        public void Stop()
        {
            Reset();
            OnInterruption?.Invoke();
        }

        public void Tick(float timeStep)
        {
            if(!_isActive) return;
            if(IsPaused) return;

            _updater.Tick(timeStep);
        }

        public void Pause()
        {
            IsPaused = true;
            OnPause?.Invoke();
        }

        public void Resume()
        {
            IsPaused = false;
            OnResume?.Invoke();
        }

        private void Complete()
        {
            Reset();
            OnCompletion?.Invoke();
        }

        private void Reset()
        {
            _isActive = false;
            IsPaused = false;
            _currentCycle = 0;
            _updater.Reset();
        }

        private void HandleCycleEnd()
        {
            OnCycleFinished?.Invoke();
            _currentCycle++;

            if(_resetStrategy == null || _resetStrategy.IsFinal())
                Complete();
        }
    }
}