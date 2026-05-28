using System.Collections.Generic;
using EggCentric.ProceduralGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public interface IGenerationNode
    {
        public IReadOnlyList<IGenerationModifier> Modifiers { get; }
        public float SampleAt(Vector3 position, float t = 0f);
    }
}
