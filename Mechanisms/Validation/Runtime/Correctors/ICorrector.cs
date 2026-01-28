namespace EggCentric.Validation
{
    public interface ICorrector<T> : IValidator<T>
    {
        public bool Validate(T item, out T validated);
    }
}
