using UnityEngine;

namespace EggCentric.Emmiters
{
    public class UniformSin : IAngleSamplingStrategy
    {
        public float GetAngle(float minAngle, float maxAngle, float t)
        {
            var minSin = Mathf.Sin(minAngle * Mathf.Deg2Rad);
            var maxSin = Mathf.Sin(maxAngle * Mathf.Deg2Rad);
            var sin = Mathf.Lerp(minSin, maxSin, t);

            return Mathf.Asin(sin) * Mathf.Rad2Deg;
        }
    }
}