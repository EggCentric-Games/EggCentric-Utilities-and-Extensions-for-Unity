using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public abstract class ValueSource : IValueSource
    {
        public abstract float SampleAt(Vector3 position);
    }
}