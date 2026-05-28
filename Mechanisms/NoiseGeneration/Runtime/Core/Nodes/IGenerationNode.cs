using System.Collections.Generic;
using EggCentric.NoiseGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public interface IGenerationNode
    {
        public IReadOnlyList<IGenerationModifier> Modifiers { get; }
        public float SampleAt(Vector3 position, float t = 0f);
    }
}
