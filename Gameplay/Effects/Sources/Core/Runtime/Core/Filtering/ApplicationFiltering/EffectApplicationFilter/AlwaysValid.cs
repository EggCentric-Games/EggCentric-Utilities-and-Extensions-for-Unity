using System.Collections.Generic;

namespace EggCentric.Effects.Sources
{
    public class AlwaysValid : IEffectApplicationFilter
    {
        public bool ValidateFor(IEffectGateway candidate, IEnumerable<IEffectView> effects) => true;
    }
}
