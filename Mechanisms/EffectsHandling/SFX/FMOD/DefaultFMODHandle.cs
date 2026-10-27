using FMOD.Studio;

using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace EggCentric.EffectsHandling.SFX.FMOD
{
    public class DefaultFMODHandle : IEffectHandle
    {
        public EventInstance Event { get; }

        public DefaultFMODHandle(EventInstance @event) => Event = @event;

        public void Play() => Event.start();
        public void Stop()
        {
            Event.stop(STOP_MODE.ALLOWFADEOUT);
            Event.release();
        }

        public void Pause(bool isPaused = true) => Event.setPaused(isPaused);
    }
}