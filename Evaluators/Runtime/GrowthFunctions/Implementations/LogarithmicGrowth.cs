using UnityEngine;

namespace EggCentric.Evaluators
{
    public class LogarithmicGrowth : IGrowth
    {
        private float baseNumber;

        public LogarithmicGrowth() : this(2f)
        {
        }

        public LogarithmicGrowth(float baseNumber)
        {
            this.baseNumber = baseNumber;
        }

        public float Evaluate(float value)
        {
            return Mathf.Log(value, baseNumber);
        }
    }
}