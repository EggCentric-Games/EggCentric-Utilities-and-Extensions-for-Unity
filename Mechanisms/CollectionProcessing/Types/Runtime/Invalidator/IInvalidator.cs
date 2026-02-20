using System.Collections.Generic;

public interface IInvalidator<TInput, TRequested>
{
    public IEnumerable<TRequested> FilterInvalid(IEnumerable<TInput> items);
    public bool Validate(TInput item, out TRequested typed);
}
