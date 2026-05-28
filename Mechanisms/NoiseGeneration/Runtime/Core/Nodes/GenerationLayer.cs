using System.Collections.Generic;
using EggCentric.NoiseGeneration.Modifiers;
using UnityEngine;

namespace EggCentric.NoiseGeneration
{
    public class GenerationLayer : GenerationNode
    {
        public IValueSource Source => _source;
        public LayerSettings LayerSettings => _layerSettings;

        private readonly IValueSource _source;
        private readonly LayerSettings _layerSettings;

        public GenerationLayer(IValueSource source, LayerSettings layerSettings)
            : this(source, layerSettings, System.Array.Empty<IGenerationModifier>())
        {
        }

        public GenerationLayer(IValueSource source, LayerSettings layerSettings, IReadOnlyList<IGenerationModifier> modifiers)
            : base(modifiers)
        {
            _source = source;
            _layerSettings = layerSettings;
        }

        protected override float GetSampleAt(Vector3 position, float t = 0f)
        {
            var value = 0f;

            foreach (var sublayer in _layerSettings.Sublayers)
            {
                var samplePosition = Vector3.Scale(position, sublayer.PositionMultiplier);
                var sample = _source.SampleAt(samplePosition + sublayer.Velocity * t);
                value += sample * sublayer.Power;
            }

            value /= _layerSettings.Normalizer;
            value *= _layerSettings.Amplitude;
            value += _layerSettings.Offset;

            return value;
        }
    }
}
