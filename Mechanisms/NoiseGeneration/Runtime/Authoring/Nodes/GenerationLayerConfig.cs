using EggCentric.ProceduralGeneration.Authoring.Modifiers;
using UnityEngine;

namespace EggCentric.ProceduralGeneration
{
    [System.Serializable]
    public class GenerationLayerConfig : IGenerationNodeConfig<GenerationLayer>
    {
        public IValueSourceConfig<IValueSource> SourceConfig => _sourceConfig;
        public Vector3 Scale => _scale;
        public Vector3 Speed => _speed;
        public float Offset => _offset;
        public float Amplitude => _amplitude;
        public OctaveSettings OctaveSettings => _octaveSettings;
        public IModifierConfig[] ModifierConfigs => _modifierConfigs;

        [SerializeField] private IValueSourceConfig<IValueSource> _sourceConfig = new PerlinNoiseConfig();
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Vector3 _speed;
        [SerializeField] private float _offset;
        [SerializeField] private float _amplitude;
        [SerializeField] private OctaveSettings _octaveSettings;
        [SerializeField] private IModifierConfig[] _modifierConfigs = new IModifierConfig[0];

        public GenerationLayer CreateInstance()
        {
            return new GenerationLayer(
                _sourceConfig.CreateInstance(),
                CreateSettings(),
                ModifierConfigUtility.CreateModifiers(_modifierConfigs));
        }
        
        private LayerSettings CreateSettings()
        {
            var sublayers = CreateSublayers();

            var settings = new LayerSettings()
            {
                Sublayers = sublayers,
                Amplitude = _amplitude,
                Offset = _offset,
                Normalizer = _octaveSettings.GetNormalizer(),
            };

            return settings;
        }

        private GenerationSublayer[] CreateSublayers()
        {
            var octaves = _octaveSettings.CreateOctaves();

            var layers = new GenerationSublayer[octaves.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                var octave = octaves[i];
                var layer = new GenerationSublayer();

                Vector3 velocity = Quaternion.AngleAxis(octave.Angle, Vector3.forward) * _speed;

                layer.PositionMultiplier = octave.Scale * _scale;
                layer.Velocity = velocity * octave.Speed;
                layer.Power = octave.Power;

                layers[i] = layer;
            }

            return layers;
        }
    }
}
