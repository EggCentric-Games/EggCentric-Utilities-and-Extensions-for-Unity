using System;

namespace EggCentric.Conditions
{
    public class SimpleCondition : ICondition
    {
        public bool IsSatisfied => _checker();

        private readonly Func<bool> _checker;

        public SimpleCondition(Func<bool> checker) => _checker = checker;

        public static implicit operator SimpleCondition(Func<bool> obj) => new SimpleCondition(obj);
    }
}