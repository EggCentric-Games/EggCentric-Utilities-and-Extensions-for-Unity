using System;
using System.Collections.Generic;
using EggCentric.NoiseGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.NoiseGeneration
{

    public abstract class GenerationNode : IGenerationNode
    {
        public IReadOnlyList<IGenerationModifier> Modifiers => _modifiers;

        private readonly IReadOnlyList<IGenerationModifier> _modifiers;

        protected GenerationNode(IReadOnlyList<IGenerationModifier> modifiers)
        {
            _modifiers = modifiers ?? Array.Empty<IGenerationModifier>();
        }

        public float SampleAt(Vector3 position, float t = 0f)
        {
            return ModifierUtility.Apply(_modifiers, GetSampleAt(position, t));
        }

        protected abstract float GetSampleAt(Vector3 position, float t = 0f);
    }
}
