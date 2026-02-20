using UnityEngine;

namespace EggCentric.Randomization
{
    public class GaussianRandom : IRandom
    {
        private readonly float _defaultDispersion = 1f;

        public GaussianRandom(float defaultDispersion = 1f) => _defaultDispersion = defaultDispersion;

        public float Range(float min = 0, float max = 1)
        {
            var mean = (min + max) / 2f;
            var delta = max - mean;

            return GetValue(mean, delta / 3f);
        }

        public float GetValue(float mean = 0) => GetValue(mean, _defaultDispersion);
        public float GetValue(float mean, float dispersion)
        {
            var u1 = Random.value;
            var u2 = Random.value;
            var z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);

            return mean + dispersion * z;
        }
    }
}
