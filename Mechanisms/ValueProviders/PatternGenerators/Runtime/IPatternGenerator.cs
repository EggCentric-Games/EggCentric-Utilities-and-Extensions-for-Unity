namespace EggCentric.ValueProviders.PatternGenerators
{
    public interface IPatternGenerator<T> : IValueProvider<T>
    {
        public T GetNext();
        public T Peek();
    }

    public abstract class PatternGenerator<T> : IPatternGenerator<T>
    {
        public T Value { get; private set; }

        public T GetNext()
        {
            Value = Peek();
            HandleItemChange();

            return Value;
        }

        public abstract T Peek();
        protected abstract void HandleItemChange();
    }
}