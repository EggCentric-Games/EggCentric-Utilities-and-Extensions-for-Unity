using System;

namespace EggCentric.OperationPerformers
{
    public abstract class OperationPerformer : IOperationPerformer
    {
        public int RepeatCount { get; set; }

        private Action _currentCycle;

        public event Action OnOperationStarted;
        public event Action OnOperationStopped;

        public void StartOperating()
        {
            if (_currentCycle != null)
                return;

            EnterLoop();
        }

        public void StopOperating()
        {
            if (_currentCycle == null)
                return;

            InterruptCycle();
        }

        protected abstract void PerformOperation();
        protected abstract void HandleSubscription(Action callback, bool isSubscribing = true);

        protected virtual void EnterLoop()
        {
            OnOperationStarted?.Invoke();

            ExecuteCycle(0, RepeatCount);
        }

        protected virtual void ExecuteCycle(int currentCycle, int maxCount = 1, bool isDeferred = false)
        {
            void Perform()
            {
                HandleSubscription(Perform, false);
                PerformOperation();

                if (currentCycle + 1 < maxCount)
                    ExecuteCycle(currentCycle + 1, maxCount, true);
                else
                    StopOperating();

            }

            _currentCycle = Perform;
            if (isDeferred)
                HandleSubscription(Perform);
            else
                Perform();
        }

        private void InterruptCycle()
        {
            HandleSubscription(_currentCycle, false);
            _currentCycle = null;

            OnOperationStopped?.Invoke();
        }
    }
}