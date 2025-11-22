using System;

namespace EggCentric.Conditions
{
    public class SimpleCondition : ICondition
    {
        public bool IsSatisfied => _checker();

        private Func<bool> _checker;

        public SimpleCondition(Func<bool> checker)
        {
            _checker = checker;
        }
    }
}