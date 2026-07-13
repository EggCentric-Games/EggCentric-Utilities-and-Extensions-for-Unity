namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.Sensors
{
    public interface IDetection<out TComponent>
    {
        public TComponent Component { get; }
    }

    public record Detection<TComponent, TSource>(TComponent Component, TSource Source) : IDetection<TComponent>
    {
        public static implicit operator TComponent(Detection<TComponent, TSource> obj) => obj.Component;
    }
}