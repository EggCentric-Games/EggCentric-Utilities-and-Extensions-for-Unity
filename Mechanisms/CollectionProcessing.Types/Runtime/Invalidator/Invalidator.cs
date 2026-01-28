using System.Collections.Generic;
using System.Linq;

public class Invalidator<TInput, TRequested> : IInvalidator<TInput, TRequested>
{
    public bool Validate(TInput item, out TRequested typed)
    {
        typed = default;
        if (item is TRequested converted)
        {
            typed = converted;
            return true;
        }

        return false;
    }

    public IEnumerable<TRequested> FilterInvalid(IEnumerable<TInput> items) => items.OfType<TRequested>();
}
