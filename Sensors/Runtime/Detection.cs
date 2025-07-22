using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.Sensors
{
    public record Detection<T>(T Component, Collider2D Collider) where T : Component
    {
        public static implicit operator T(Detection<T> obj)
        {
            return obj.Component;
        }
    }
}