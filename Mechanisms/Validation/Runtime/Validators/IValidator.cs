namespace EggCentric.Validation
{
    public interface IValidator<T>
    {
        public bool Validate(T item);
    }
}
