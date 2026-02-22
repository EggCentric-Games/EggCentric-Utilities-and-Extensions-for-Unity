using UnityEngine;

namespace EggCentric.Emmiters
{
    public class UniformCos : IAngleSamplingStrategy
    {
        public float GetAngle(float minAngle, float maxAngle, float t)
        {
            var minCos = Mathf.Cos(minAngle * Mathf.Deg2Rad);
            var maxCos = Mathf.Cos(maxAngle * Mathf.Deg2Rad);
            var cos = Mathf.Lerp(minCos, maxCos, t);

            return Mathf.Acos(cos) * Mathf.Rad2Deg;
        }
    }
}