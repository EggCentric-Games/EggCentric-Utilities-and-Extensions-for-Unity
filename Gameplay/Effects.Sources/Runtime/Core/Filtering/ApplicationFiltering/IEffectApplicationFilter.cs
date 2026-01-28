using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public interface IEffectApplicationFilter
    {
        public bool ValidateFor(IEffectGateway candidate, IEnumerable<IEffectView> effects);
    }
}
