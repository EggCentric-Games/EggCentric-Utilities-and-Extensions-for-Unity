using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public interface IValueSource
    {
        public float SampleAt(Vector3 position);
    }
}