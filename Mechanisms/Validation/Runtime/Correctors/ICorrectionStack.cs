namespace EggCentric.Validation
{
    public interface ICorrectionStack<T> : IReadOnlyCorrectionStack<T>, IItemStack<ICorrector<T>>
    {

    }
}
