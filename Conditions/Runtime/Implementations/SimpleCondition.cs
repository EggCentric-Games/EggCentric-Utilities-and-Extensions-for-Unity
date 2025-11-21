using System;

namespace EggCentric.Conditions
{
    public class SimpleCondition : ICondition
    {
        private Func<bool> _checker;

        public SimpleCondition(Func<bool> checker)
        {
            _checker = checker;
        }

        public bool Check()
        {
            return _checker.Invoke();
        }
    }
}