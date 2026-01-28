using EggCentric.Conditions;
using System;
using System.Collections.Generic;

namespace EggCentric.StateMachines
{
    public class Transition<TTarget> : ITransition<TTarget> where TTarget : class, IState
    {
        public bool IsSatisfied => CheckConditions();

        private List<ICondition> _conditions;

        public Transition()
        {
            _conditions = new List<ICondition>();
        }

        public Transition<TTarget> WithCondition(ICondition condition)
        {
            _conditions.Add(condition);

            return this;
        }

        public Transition<TTarget> WithCondition(Func<bool> _checker)
        {
            ICondition condition = new SimpleCondition(_checker);
            return WithCondition(condition);
        }

        private bool CheckConditions()
        {
            if (_conditions.Count <= 0)
                return true;

            foreach (var condition in _conditions)
            {
                if (!condition.IsSatisfied)
                    return false;
            }

            return true;
        }
    }
}