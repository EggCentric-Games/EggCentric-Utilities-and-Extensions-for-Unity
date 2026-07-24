using UnityEngine;

namespace EggCentric.Sensors
{
    public abstract class CastingProbe<TSettings, THit> : IProbe<THit> where THit : struct
    {
        public TSettings Settings { get; }

        public CastingProbe(TSettings settings)
        {
            Settings = settings;
        }

        public abstract bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out THit hit);
        public abstract THit[] CastAll(Vector3 origin, Vector3 direction, float maxDistance);
    }
}
