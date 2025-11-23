using System;

namespace EggCentric.StateMachines
{
    public interface ITransition
    {
        public bool IsSatisfied { get; }
        public Type TargetState { get; }
    }

    public interface ITransition<TTarget> : ITransition where TTarget : class, IState
    {
        Type ITransition.TargetState => typeof(TTarget);
    }
}