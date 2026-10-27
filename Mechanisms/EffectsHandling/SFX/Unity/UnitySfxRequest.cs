using UnityEngine;

namespace EggCentric.EffectsHandling.SFX.Unity
{
    public class UnitySfxRequest : EffectRequest<DefaultSfxConfig>
    {
        public DefaultSfxConfig Config { get; }
        public Vector3 Position { get; }

        public UnitySfxRequest(DefaultSfxConfig config, Vector3 position) : base(config)
        {
            Config = config;
            Position = position;
        }
    }
}