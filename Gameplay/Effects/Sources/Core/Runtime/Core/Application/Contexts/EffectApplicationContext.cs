namespace EggCentric.Effects.Sources
{
    public class EffectApplicationContext<TDetectionContext> : IEffectApplicationContext<TDetectionContext> where TDetectionContext : IDetectionContext
    {
        public IEffectLayerContext LayerContext { get; }
        public TDetectionContext DetectionContext { get; }

        public EffectApplicationContext(IEffectLayerContext layerContext, TDetectionContext detectionContext)
        {
            LayerContext = layerContext;
            DetectionContext = detectionContext;
        }
    }
}