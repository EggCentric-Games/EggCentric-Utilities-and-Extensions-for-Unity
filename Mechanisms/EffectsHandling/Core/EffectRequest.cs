namespace EggCentric.EffectsHandling
{
    public abstract class EffectRequest<TConfig> : IEffectRequest where TConfig : IEffectConfig
    {
        public EffectRequest(TConfig config)
        {
        }
    }
}