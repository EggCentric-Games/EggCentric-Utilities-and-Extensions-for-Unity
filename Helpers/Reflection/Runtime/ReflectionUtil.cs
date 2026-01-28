using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace EggCentric.Reflection
{
    public static class ReflectionUtil
    {
        public static List<Type> GetSubclassesOfType<T>() where T : class
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in
                Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsDerivedFrom(typeof(T))))
            {
                objects.Add(type);
            }

            return objects;
        }
    }
}