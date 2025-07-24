using UnityEngine;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}

namespace EggCentric.DataContainers
{
    public record DataRecord<TValue>(TValue Data, float Timestamp, float TimeToLive)
    {
        public bool IsValid => Time.time <= Timestamp + TimeToLive;

        public DataRecord(TValue data, float timeToLive = 0f) : this(data, Time.time, timeToLive)
        {

        }

        public static implicit operator TValue(DataRecord<TValue> obj)
        {
            return obj.Data;
        }
    }
}