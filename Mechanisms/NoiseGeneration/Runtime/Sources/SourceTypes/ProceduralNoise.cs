using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public abstract class ProceduralNoise : ValueSource
    {
        public ulong Seed { get; set; }

        public override float SampleAt(Vector3 position) => GetSample(position, Seed);

        protected abstract float GetSample(Vector3 position, ulong seed);
    }
}