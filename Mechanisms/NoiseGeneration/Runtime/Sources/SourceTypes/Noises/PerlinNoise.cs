using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class PerlinNoise : ProceduralNoise
    {
        private static readonly Vector3[] Gradients =
        {
            new( 1, 1, 0),
            new(-1, 1, 0),
            new( 1,-1, 0),
            new(-1,-1, 0),

            new( 1, 0, 1),
            new(-1, 0, 1),
            new( 1, 0,-1),
            new(-1, 0,-1),

            new( 0, 1, 1),
            new( 0,-1, 1),
            new( 0, 1,-1),
            new( 0,-1,-1)
        };

        protected override float GetSample(Vector3 position, ulong seed)
        {
            var xF = Mathf.FloorToInt(position.x);
            var yF = Mathf.FloorToInt(position.y);
            var zF = Mathf.FloorToInt(position.z);

            var xC = xF + 1;
            var yC = yF + 1;
            var zC = zF + 1;

            float dx = position.x - xF;
            float dy = position.y - yF;
            float dz = position.z - zF;

            float n000 = GradientDot(xF, yF, zF, dx, dy, dz, seed);
            float n001 = GradientDot(xF, yF, zC, dx, dy, dz - 1, seed);
            float n010 = GradientDot(xF, yC, zF, dx, dy - 1, dz, seed);
            float n011 = GradientDot(xF, yC, zC, dx, dy - 1, dz - 1, seed);
            float n100 = GradientDot(xC, yF, zF, dx - 1, dy, dz, seed);
            float n101 = GradientDot(xC, yF, zC, dx - 1, dy, dz - 1, seed);
            float n110 = GradientDot(xC, yC, zF, dx - 1, dy - 1, dz, seed);
            float n111 = GradientDot(xC, yC, zC, dx - 1, dy - 1, dz - 1, seed);

            var u = Fade(dx);
            var v = Fade(dy);
            var w = Fade(dz);

            float zAvg1 = Mathf.Lerp(n000, n001, w);
            float zAvg2 = Mathf.Lerp(n010, n011, w);
            float zAvg3 = Mathf.Lerp(n100, n101, w);
            float zAvg4 = Mathf.Lerp(n110, n111, w);

            float yAvg1 = Mathf.Lerp(zAvg1, zAvg2, v);
            float yAvg2 = Mathf.Lerp(zAvg3, zAvg4, v);

            float average = Mathf.Lerp(yAvg1, yAvg2, u);

            return average;
        }

        private float GradientDot(float x, float y, float z, float dx, float dy, float dz, ulong seed)
        {
            var hash = NoiseHash.Hash(stackalloc float[] { x, y, z }, seed);
            var gradient = Gradients[(int)(hash % (ulong)Gradients.Length)];
            var influence = gradient.x * dx + gradient.y * dy + gradient.z * dz;

            return influence;
        }

        private float Fade(float t)
        {
            return t * t * t * (t * (t * 6f - 15f) + 10f);
        }
    }
}