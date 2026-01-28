namespace EggCentric.DI
{
    public class ContainerFactory
    {
        public static IContainer CreateNew(IReadOnlyContainer parentContainer = null) => new NaiveContainer(parentContainer);
    }
}
