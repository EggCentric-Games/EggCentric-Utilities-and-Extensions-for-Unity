namespace EggCentric.Effects.Sources
{
    public interface IEffectExecutionPolicy<TDetectionContext> where TDetectionContext : IDetectionContext
    {
        public bool ValidateTarget(TDetectionContext entry);
        public void HandleRequestExecution(IEffectApplicationContext<TDetectionContext> entry);
    }
}