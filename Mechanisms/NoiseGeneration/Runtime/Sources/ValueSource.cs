using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public abstract class ValueSource : IValueSource
    {
        public abstract float SampleAt(Vector3 position);
    }
}