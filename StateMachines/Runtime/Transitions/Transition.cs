using EggCentric.Conditions;
using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public class Transition
    {
        public Type TargetState { get; private set; }
        public bool IsSatisfied => CheckConditions();

        private List<ICondition> _conditions = new List<ICondition>();

        public Transition To<TState>() where TState : IState
        {
            TargetState = typeof(TState);

            return this;
        }

        public Transition WithCondition(ICondition condition)
        {
            _conditions.Add(condition);

            return this;
        }

        public Transition WithCondition(Func<bool> _checker)
        {
            ICondition condition = new SimpleCondition(_checker);
            return WithCondition(condition);
        }

        private bool CheckConditions()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.IsSatisfied)
                    return false;
            }

            return true;
        }
    }
}