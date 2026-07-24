using UnityEngine;

namespace EggCentric.Sensors
{
    public interface IProbe<THit> where THit : struct
    {
        public bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out THit hit);
        public THit[] CastAll(Vector3 origin, Vector3 direction, float maxDistance);
    }
}
