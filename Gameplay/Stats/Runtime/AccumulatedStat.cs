using UnityEngine;

namespace EggCentric.Stats
{
    public class AccumulatedStat : RangedStat
    {
        protected float accumulationRate;
        protected float lossRate;

        public AccumulatedStat(float min, float max, float accumulationRate, float lossRate, float value = 0) : base(min, max, value)
        {
            this.accumulationRate = accumulationRate;
            this.lossRate = lossRate;
        }

        public void DoTick(float tickLength, float tickCoefficient)
        {
            if (tickLength <= 0 || tickCoefficient == 0)
                return;

            if (tickCoefficient > 0)
                ChargeUp(tickLength * tickCoefficient);
            else
                ChargeDown(tickLength * tickCoefficient);
        }

        public void ChargeUp(float tickLength)
        {
            if (tickLength == 0)
                return;

            if (lossRate == Mathf.Infinity)
                SetValue(upperLimit);
            else
                ChangeValue(tickLength * accumulationRate);
        }

        public void ChargeDown(float tickLength)
        {
            if (tickLength == 0)
                return;

            if (lossRate == Mathf.Infinity)
                SetValue(lowerLimit);
            else
                ChangeValue(-(tickLength * lossRate));
        }
    }
}