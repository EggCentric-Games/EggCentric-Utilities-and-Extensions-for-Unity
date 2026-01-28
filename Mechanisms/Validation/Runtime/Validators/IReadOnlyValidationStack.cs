namespace EggCentric.Validation
{
    public interface IReadOnlyValidationStack<T> : IValidator<T>, IReadOnlyItemStack<IValidator<T>>
    {

    }
}
