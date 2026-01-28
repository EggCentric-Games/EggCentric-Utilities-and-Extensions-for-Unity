using System;

namespace EggCentric.Conditions
{
    public class Condition<TKey> : ICondition
    {
        public bool IsSatisfied => _comparer(_getter());

        private Func<TKey> _getter;
        private Func<TKey, bool> _comparer;

        public Condition(Func<TKey> getter, Func<TKey, bool> comparer)
        {
            _getter = getter;
            _comparer = comparer;
        }
    }
}