namespace EggCentric.PeriodicUpdaters
{
    public class FixedPeriodProvider : FixedValueProvider<float>, IUpdatePeriodProvider
    {
        public FixedPeriodProvider(float value) : base(value)
        {
        }

        public float GetNext() => Value;
        public void Reset() { }
    }
}