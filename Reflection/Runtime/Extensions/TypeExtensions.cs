using System;

namespace EggCentric.Reflection
{
    public static class TypeExtensions
    {
        public static bool IsDerivedFrom(this Type a, Type b)
        {
            return b.IsAssignableFrom(a);
        }
    }
}