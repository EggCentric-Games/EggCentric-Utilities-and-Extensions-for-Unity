namespace EggCentric.Effects.Sources
{
    public interface IEffectApplicationContext<out TDetectionContext> where TDetectionContext : IDetectionContext
    {
        public IEffectLayerContext LayerContext { get; }
        public TDetectionContext DetectionContext { get; }
    }
}