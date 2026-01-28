using EggCentric.DI;
using System.Collections.Generic;

namespace EggCentric.Effects
{
    public interface IEffectView
    {
        public IReadOnlyContainer Context { get; }
        public IEnumerable<IEffectComponentView> Components { get; }
    }
}
