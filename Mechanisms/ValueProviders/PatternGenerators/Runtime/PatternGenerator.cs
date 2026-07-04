namespace EggCentric.ValueProviders.PatternGenerators
{
    public abstract class PatternGenerator<T> : IPatternGenerator<T>
    {
        public T Value { get; private set; }

        public T GetNext()
        {
            Value = Peek();
            HandleItemChange();

            return Value;
        }

        public abstract T Peek(int offset = 0);
        protected abstract void HandleItemChange();
    }
}