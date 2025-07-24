using System;

namespace EggCentric.Stats
{
    public class RangedStat : ValueStat
    {
        public float Percentage => (currentValue - lowerLimit) / (upperLimit - lowerLimit);
        public float LowerLimit => lowerLimit;
        public float UpperLimit => upperLimit;

        protected float lowerLimit;
        protected float upperLimit;

        public event Action OnLowerLimitReached;
        public event Action OnLowerLimitDisrupted;
        public event Action OnUpperLimitReached;
        public event Action OnUpperLimitDisrupted;

        public RangedStat(float min, float max, float value = 0f) : base(value)
        {
            this.lowerLimit = min;
            this.upperLimit = max;
        }

        protected override void IncreaseValue(float amount)
        {
            float oldValue = currentValue;
            float capacityToLimit = upperLimit - currentValue;
            float limitedChange = Math.Min(amount, capacityToLimit);

            if (limitedChange == 0)
                return;

            base.IncreaseValue(limitedChange);

            if (oldValue == lowerLimit)
                OnLowerLimitDisrupted?.Invoke();

            if (limitedChange == capacityToLimit)
                OnUpperLimitReached?.Invoke();
        }

        protected override void DecreaseValue(float amount)
        {
            float oldValue = currentValue;
            float capacityToLimit = currentValue - lowerLimit;
            float limitedChange = Math.Min(amount, capacityToLimit);

            if (limitedChange == 0)
                return;

            base.DecreaseValue(limitedChange);

            if (oldValue == upperLimit)
                OnUpperLimitDisrupted?.Invoke();

            if (limitedChange == capacityToLimit)
                OnLowerLimitReached?.Invoke();
        }
    }
}