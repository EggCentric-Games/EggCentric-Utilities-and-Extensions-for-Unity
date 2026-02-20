namespace EggCentric.Validation
{
    public delegate bool Corrector<T>(T input, out T output);
}
