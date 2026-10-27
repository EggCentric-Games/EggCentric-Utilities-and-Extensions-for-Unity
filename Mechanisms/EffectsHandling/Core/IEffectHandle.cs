namespace EggCentric.EffectsHandling
{
    public interface IEffectHandle
    {
        public void Play();
        public void Pause(bool isPaused = true);
        public void Stop();
    }
}