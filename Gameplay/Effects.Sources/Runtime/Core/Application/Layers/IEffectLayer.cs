using EggCentric.Common.Behaviours;

namespace EggCentric.Effects.Sources
{
    public interface IEffectLayer<TApplicationContext> : IApplicable<TApplicationContext> where TApplicationContext : IDetectionContext
    {

    }
}