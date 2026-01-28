using UnityEngine;

namespace EggCentric.Randomization
{
    public interface IRandomStrategy
    {
        public void Range(float min = 0f, float max = 1f);
    }

    public class UniformRandom : IRandomStrategy
    {
        public void Range(float min = 0, float max = 1) => Random.Range(min, max);
    }

    public class GaussRandom : IRandomStrategy
    {
        private readonly float _defaultDispersion = 1f;

        public GaussRandom(float defaultDispersion = 1f) => _defaultDispersion = defaultDispersion;

        public void Range(float min = 0, float max = 1) => GetValue((min + max) / 2f);

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
