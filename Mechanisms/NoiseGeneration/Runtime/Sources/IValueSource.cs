using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public interface IValueSource
    {
        public float SampleAt(Vector3 position);
    }
}