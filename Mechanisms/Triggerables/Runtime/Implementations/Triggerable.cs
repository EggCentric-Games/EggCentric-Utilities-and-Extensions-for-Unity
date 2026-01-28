using EggCentric.DataContainers;
using System;

namespace EggCentric.Triggerables
{
    public abstract class Triggerable : ITriggerable
    {
        public bool IsReady => isReady;

        protected Field<bool> isReady;

        public event Action OnTriggerSet;
        public event Action OnTriggerReleased;

        public Triggerable()
        {
            isReady = new Field<bool>(false);

            isReady.OnValueChanged += HandleReadinessNotification;
        }

        public bool TryToRelease()
        {
            if (!IsReady)
                return false;

            Release();
            return true;
        }

        protected virtual void Release()
        {
            isReady.Value = false;
            OnTriggerReleased?.Invoke();
        }

        protected void HandleReadinessNotification(bool isReady)
        {
            if (isReady)
                OnTriggerSet?.Invoke();
        }

        public abstract void EvaluateReadiness();
    }
}