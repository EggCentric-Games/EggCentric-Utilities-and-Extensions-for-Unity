using UnityEngine;

namespace EggCentric.Randomization
{
    public class UniformRandom : IRandom
    {
        public float Range(float min = 0, float max = 1) => Random.Range(min, max);
    }
}
