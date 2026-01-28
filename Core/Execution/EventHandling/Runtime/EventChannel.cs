using System;
using UnityEngine;

namespace EggCentric.EventHandling
{
    [CreateAssetMenu(fileName = "VoidChannel_", menuName = "EggCentric/EventHandling/VoidEventChannel", order = 1)]
    public class EventChannel : ScriptableObject, IEventChannel
    {
        public event Action OnRaisedNoArg = null;

        public virtual void RaiseEvent()
        {
            OnRaisedNoArg?.Invoke();
        }
    }

    public class EventChannel<T> : EventChannel
    {
        public event Action<T> OnRaised = null;

        public override void RaiseEvent()
        {
            RaiseEvent(default);
        }

        public void RaiseEvent(T arg)
        {
            base.RaiseEvent();
            OnRaised?.Invoke(arg);
        }
    }
}