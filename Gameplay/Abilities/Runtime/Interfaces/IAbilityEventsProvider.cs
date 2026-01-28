using System;

namespace EggCentric.Abilities
{
    public interface IAbilityEventsProvider
    {
        public event Action OnExecutionRequested;
        public event Action OnExecutionDenied;
        public event Action OnExecutionStarted;
        public event Action OnExecutionInterrupted;
        public event Action OnExecutionCompleted;
    }
}