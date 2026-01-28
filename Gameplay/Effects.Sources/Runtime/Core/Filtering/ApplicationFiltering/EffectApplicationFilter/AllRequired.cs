using System.Collections.Generic;
using System.Linq;

namespace EggCentric.Effects.Sources
{
    public class AllRequired : IEffectApplicationFilter
    {
        public bool ValidateFor(IEffectGateway candidate, IEnumerable<IEffectView> effects) => effects.All(x => candidate.IsApplicable(x));
    }
}
