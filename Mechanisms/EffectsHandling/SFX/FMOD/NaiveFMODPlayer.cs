using FMODUnity;

namespace EggCentric.EffectsHandling.SFX.FMOD
{
    public class NaiveFMODPlayer : IEffectPlayer<DefaultFMODRequest, DefaultFMODHandle>
    {
        public DefaultFMODHandle CreatePersistent(DefaultFMODRequest sample)
        {
            return new DefaultFMODHandle(RuntimeManager.CreateInstance(sample.Event));
        }

        public void PlayOnce(DefaultFMODRequest sample)
        {
            RuntimeManager.PlayOneShot(sample.Event, sample.Position);
        }
    }
}