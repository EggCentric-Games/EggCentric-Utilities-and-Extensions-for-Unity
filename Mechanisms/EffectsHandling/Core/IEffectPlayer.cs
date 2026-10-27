namespace EggCentric.EffectsHandling
{
    public interface IEffectPlayer<in TRequest, out THandle>where TRequest : IEffectRequest where THandle : IEffectHandle
    {
        public void PlayOnce(TRequest sample);
        public THandle CreatePersistent(TRequest sample);
    }
}