using UnityEngine;

namespace EggCentric.EffectsHandling.VFX
{
    public class DefaultVFXRequest : EffectRequest<DefaultVFXConfig>
    {
        public DefaultVFXConfig Config { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }

        public DefaultVFXRequest(DefaultVFXConfig config, Vector3 position, Quaternion rotation) : base(config)
        {
            Config = config;
            Position = position;
            Rotation = rotation;
        }
    }
}