using System;

namespace EggCentric.Conditions
{
    public class Condition<TKey> : ICondition
    {
        private Func<TKey> _getter;
        private Func<TKey, bool> _comparer;

        public Condition(Func<TKey> getter, Func<TKey, bool> comparer)
        {
            _getter = getter;
            _comparer = comparer;
        }

        public bool Check()
        {
            return _comparer.Invoke(_getter.Invoke());
        }
    }
}