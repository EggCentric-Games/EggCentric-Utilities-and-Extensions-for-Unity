using UnityEngine;

namespace EggCentric.EffectsHandling.SFX.Unity
{
    public class DefaultSfxConfig : IEffectConfig
    {
        public AudioClip AudioClip => _audioClip;
        public Vector2 PitchRange => _pitchRange;
        public Vector2 VolumeRange => _volumeRange;

        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private Vector2 _pitchRange;
        [SerializeField] private Vector2 _volumeRange;
    }

    public class DefaultUnitySfxHandle : IEffectHandle
    {
        public AudioSource Source { get; }

        public DefaultUnitySfxHandle(AudioSource source) => Source = source;

        public void Play() => Source.Play();
        public void Stop() => Source.Stop();

        public void Pause(bool isPaused = true)
        {
            if(isPaused)
                Source.Pause();
            else
                Source.UnPause();
        }
    }
}