namespace EggCentric.Validation
{
    public interface IReadOnlyCorrectionStack<T> : ICorrector<T>, IReadOnlyItemStack<ICorrector<T>>
    {

    }
}
