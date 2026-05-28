using System;
using EggCentric.ProceduralGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    public sealed class EmptyGenerationNode : GenerationNode
    {
        public static readonly EmptyGenerationNode Instance = new EmptyGenerationNode();

        private EmptyGenerationNode()
            : base(Array.Empty<IGenerationModifier>())
        {
        }

        protected override float GetSampleAt(Vector3 position, float t = 0f) => 0f;
    }
}
