using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class AnyAccepted : IEffectApplicationFilter
    {
        public bool ValidateFor(IEffectGateway candidate, IEnumerable<IEffectView> effects) => effects.Any(x => candidate.IsApplicable(x));
    }
}
