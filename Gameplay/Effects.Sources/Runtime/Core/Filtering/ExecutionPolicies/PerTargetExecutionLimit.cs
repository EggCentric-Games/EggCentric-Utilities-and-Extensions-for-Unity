using EggCentric.CollectionProcessing.Types;

namespace EggCentric.Effects.Sources
{
    public class PerTargetExecutionLimit<TDetectionContext> : IEffectExecutionPolicy<TDetectionContext> where TDetectionContext : IDetectionContext
    {
        private readonly ApplicationCountLimiter<IEffectGateway> _limiter;

        public PerTargetExecutionLimit(ApplicationCountLimiter<IEffectGateway> limiter) => _limiter = limiter;

        public bool ValidateTarget(TDetectionContext entry) => _limiter.CheckItemValidity(entry.Target);
        public void HandleRequestExecution(IEffectApplicationContext<TDetectionContext> entry) => _limiter.RegisterApplicationOnItem(entry.DetectionContext.Target);

    }
}