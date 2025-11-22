using System;

namespace EggCentric.StateMachines
{
    public interface ITransition
    {
        public Type TargetState { get; }
        public bool IsSatisfied { get; }
    }
}