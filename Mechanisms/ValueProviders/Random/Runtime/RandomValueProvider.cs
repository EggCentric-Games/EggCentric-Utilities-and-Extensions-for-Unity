using EggCentric.QoL;
using EggCentric.Randomization;

namespace EggCentric.ValueProviders.Random
{
    public class RandomValueProvider : IValueProvider<float>
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public float Value => GetNext();

        private IRandom _random;
        private IDeviationStrategy _deviationStrategy;

        public RandomValueProvider(float min = 0f, float max = 1f) : this(min, max, RandomTypes.Uniform, new MiddleDeviation()) { }
        public RandomValueProvider(float min, float max, IRandom random, IDeviationStrategy deviationStrategy)
        {
            Min = min;
            Max = max;

            _random = random;
            _deviationStrategy = deviationStrategy;
        }

        public RandomValueProvider WithStrategy(IDeviationStrategy deviationStrategy)
        {
            if (deviationStrategy == null)
                return this;

            _deviationStrategy = deviationStrategy;
            return this;
        }

        public RandomValueProvider WithRandom(IRandom random)
        {
            if (random == null)
                return this;

            _random = random;
            return this;
        }

        public float GetNext() => GetRawValue().Remap(0f, 1f, Min, Max);
        private float GetRawValue() => _deviationStrategy.GetValue(_random);
    }
}