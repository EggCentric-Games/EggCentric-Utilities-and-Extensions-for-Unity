using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.DataContainers
{
    public record DataCache<T>(T Data, float Timestamp, float TimeToLive) : IDataCache<T>
    {
        public bool IsValid => Time.time <= Timestamp + TimeToLive;

        public DataCache(T data, float timeToLive = 0f) : this(data, Time.time, timeToLive)
        {
        }
    }
}