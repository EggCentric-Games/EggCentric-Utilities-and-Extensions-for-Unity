using UnityEngine;

namespace EggCentric.Evaluators
{
    public class PowerGrowth : IGrowth
    {
        private float power;

        public PowerGrowth() : this(2f)
        {
        }

        public PowerGrowth(float power)
        {
            this.power = power;
        }

        public float Evaluate(float value)
        {
            return Mathf.Pow(value, power);
        }
    }
}