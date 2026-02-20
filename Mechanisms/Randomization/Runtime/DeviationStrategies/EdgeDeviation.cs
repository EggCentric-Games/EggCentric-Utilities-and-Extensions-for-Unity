using UnityEngine;

namespace EggCentric.Randomization
{
    public class EdgeDeviation : IDeviationStrategy
    {
        public float GetValue(IRandom random)
        {
            var deviationMagnitude = random.Range(-1f, 1f);
            return Mathf.Abs(deviationMagnitude);
        }
    }
}