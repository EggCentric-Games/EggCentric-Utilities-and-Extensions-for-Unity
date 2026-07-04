namespace EggCentric.ValueProviders.PatternGenerators
{
    public class FixedValueProvider<T> : PatternGenerator<T>
    {
        private T _value;

        public FixedValueProvider(T value) => SetValue(value);
        public void SetValue(T value) => _value = value;

        public override T Peek(int _) => _value;
        protected override void HandleItemChange() { }
    }
}