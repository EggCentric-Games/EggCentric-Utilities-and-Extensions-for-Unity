using EggCentric.DI;
using UnityEngine;

namespace EggCentric.Effects.Sources
{
    public interface IAreaDetectionContext : IDetectionContext
    {
        public Vector3 RelativePosition { get; }
    }

    public abstract class AoE : EffectSource<IAreaDetectionContext>
    {
        protected AoE(IReadOnlyContainer parentContext) : base(parentContext)
        {
        }
    }
}
