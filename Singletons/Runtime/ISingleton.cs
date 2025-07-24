namespace EggCentric.Singletons
{
    public interface ISingleton<T> where T : ISingleton<T>
    {
    }
}