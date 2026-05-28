using System.Collections.Generic;

namespace EggCentric.ValueProviders.PatternGenerators
{
    public class AutomatedSequentialValueProvider<T> : IValueProvider<T>
    {
        public T Value
        {
            get
            {
                var value = _valueProvider.Value;
                _valueProvider.GetNext();
                return value;
            }
        }

        private readonly SequentialValueProvider<T> _valueProvider;

        public AutomatedSequentialValueProvider(IEnumerable<T> sequence) => _valueProvider = new SequentialValueProvider<T>(sequence);

        public void Reset() => _valueProvider.Reset();
    }
}