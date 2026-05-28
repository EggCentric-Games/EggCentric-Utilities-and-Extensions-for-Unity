using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace EggCentric.ProceduralGeneration
{
    public class PerlinNoise : ProceduralNoise
    {
        protected override float GetSample(Vector3 position, ulong seed)
        {
            var xF = Mathf.FloorToInt(position.x);
            var yF = Mathf.FloorToInt(position.y);
            var zF = Mathf.FloorToInt(position.z);

            var xC = xF + 1;
            var yC = yF + 1;
            var zC = zF + 1;

            Vector3Int[] referencePoints = new Vector3Int[] {
                new Vector3Int(xF, yF, zF),
                new Vector3Int(xF, yF, zC),
                new Vector3Int(xF, yC, zF),
                new Vector3Int(xF, yC, zC),
                new Vector3Int(xC, yF, zF),
                new Vector3Int(xC, yF, zC),
                new Vector3Int(xC, yC, zF),
                new Vector3Int(xC, yC, zC)
            };

            float[] influences = new float[referencePoints.Length];
            for(int i = 0; i < referencePoints.Length; i++)
            {
                var hash = NoiseHash.Hash(stackalloc float[] { referencePoints[i].x, referencePoints[i].y, referencePoints[i].z }, seed);
                var gradient = RandomExtensions.WithSeed(() => Random.onUnitSphere, (int)hash);
                var offset = position - referencePoints[i];
                influences[i] = Vector3.Dot(gradient, offset);
            }

            var u = Fade(position.x - Mathf.Floor(position.x));
            var v = Fade(position.y - Mathf.Floor(position.y));
            var w = Fade(position.z - Mathf.Floor(position.z));

            float zAvg1 = Mathf.Lerp(influences[0], influences[1], w);
            float zAvg2 = Mathf.Lerp(influences[2], influences[3], w);
            float zAvg3 = Mathf.Lerp(influences[4], influences[5], w);
            float zAvg4 = Mathf.Lerp(influences[6], influences[7], w);

            float yAvg1 = Mathf.Lerp(zAvg1, zAvg2, v);
            float yAvg2 = Mathf.Lerp(zAvg3, zAvg4, v);

            float average = Mathf.Lerp(yAvg1, yAvg2, u);

            return average;
        }
        private float Fade(float t)
        {
            return t * t * t * (t * (t * 6f - 15f) + 10f);
        }
    }
}

public static class RandomExtensions
{
    public static T WithSeed<T>(Func<T> func, int seed)
    {
        var previousState = Random.state;
        Random.InitState(seed);
        var result = func();
        Random.state = previousState;

        return result;
    }
}