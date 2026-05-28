using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class RandomNoise : ProceduralNoise
    {
        protected override float GetSample(Vector3 position, ulong seed) => NoiseHash.Hash01f(stackalloc float[] { position.x, position.z }, seed);
    }
}