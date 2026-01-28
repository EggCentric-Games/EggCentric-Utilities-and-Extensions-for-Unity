using EggCentric.Common.Behaviours;
using EggCentric.DI;
using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public interface IEffectSource<TDetectionContext> : IApplicable<IEffectGateway> where TDetectionContext : IDetectionContext
    {
        public IReadOnlyContainer Context { get; }
        public IReadOnlyList<IEffectLayer<TDetectionContext>> EffectLayers { get; }
    }
}