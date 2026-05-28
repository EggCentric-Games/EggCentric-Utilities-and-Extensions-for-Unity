using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class CheckerboardNoise : ProceduralNoise
    {
        protected override float GetSample(Vector3 position, ulong seed)
        {
            Debug.LogError($"Isn't implemented yet. Should return checkerboard pattern");
            return 0;
        }
    }
}