using System;

namespace EggCentric.ValueProviders
{
    public class Getter<T> : IValueProvider<T>
    {
        public T Value => _getter();

        private readonly Func<T> _getter;

        public Getter(Func<T> getter) => _getter = getter;
    }
}