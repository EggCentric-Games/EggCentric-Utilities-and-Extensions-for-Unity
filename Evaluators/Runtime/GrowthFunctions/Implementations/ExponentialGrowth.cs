using UnityEngine;

namespace EggCentric.Evaluators
{
    public class ExponentialGrowth : IGrowth
    {
        private float exponent;

        public ExponentialGrowth() : this(2f)
        {
        }

        public ExponentialGrowth(float exponent)
        {
            this.exponent = exponent;
        }

        public float Evaluate(float value)
        {
            return Mathf.Pow(exponent, value);
        }
    }
}