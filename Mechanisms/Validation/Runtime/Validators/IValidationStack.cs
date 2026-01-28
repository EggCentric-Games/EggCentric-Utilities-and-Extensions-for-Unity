namespace EggCentric.Validation
{
    public interface IValidationStack<T> : IReadOnlyValidationStack<T>, IItemStack<IValidator<T>>
    {

    }
}
