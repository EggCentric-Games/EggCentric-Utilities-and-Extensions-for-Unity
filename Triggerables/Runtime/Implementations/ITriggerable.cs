using System;

namespace EggCentric.Triggerables
{
    public interface ITriggerable
    {
        public bool IsReady { get; }

        public event Action OnTriggerSet;
        public event Action OnTriggerReleased;

        public bool TryToRelease();
    }
}