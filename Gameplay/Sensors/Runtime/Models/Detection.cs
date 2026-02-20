using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.Sensors
{
    public interface IDetection<out T>
    {
        public T Component { get; }
        public Collider2D Collider { get; }
    }

    public record Detection<T>(T Component, Collider2D Collider) : IDetection<T>
    {
        public static implicit operator T(Detection<T> obj)
        {
            return obj.Component;
        }
    }
}