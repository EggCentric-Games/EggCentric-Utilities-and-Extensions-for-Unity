using UnityEngine;

namespace EggCentric.Emmiters
{
    public class UniformTan : IAngleSamplingStrategy
    {
        public float GetAngle(float minAngle, float maxAngle, float t)
        {
            var minTan = Mathf.Tan(minAngle * Mathf.Deg2Rad);
            var maxTan = Mathf.Tan(maxAngle * Mathf.Deg2Rad);
            var tan = Mathf.Lerp(minTan, maxTan, t);

            return Mathf.Atan(tan) * Mathf.Rad2Deg;
        }
    }
}