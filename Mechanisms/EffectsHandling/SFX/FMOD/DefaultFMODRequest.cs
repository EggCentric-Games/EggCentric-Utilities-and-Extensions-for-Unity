using FMODUnity;
using UnityEngine;

namespace EggCentric.EffectsHandling.SFX.FMOD
{
    public class DefaultFMODRequest : EffectRequest<DefaultFMODSampleConfig>
    {
        public EventReference Event { get; }
        public Vector3 Position { get; }

        public DefaultFMODRequest(DefaultFMODSampleConfig config, Vector3 position) : base(config)
        {
            Event = config.Event;
            Position = position;
        }
    }
}