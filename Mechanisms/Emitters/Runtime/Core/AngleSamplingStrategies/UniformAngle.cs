using UnityEngine;

namespace EggCentric.Emmiters
{
    public class UniformAngle : IAngleSamplingStrategy
    {
        public float GetAngle(float minAngle, float maxAngle, float t) => Mathf.Lerp(minAngle, maxAngle, t);
    }
}