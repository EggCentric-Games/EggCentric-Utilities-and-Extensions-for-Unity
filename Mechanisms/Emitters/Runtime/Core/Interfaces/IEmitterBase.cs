using UnityEngine;

namespace EggCentric.Emmiters
{
    public interface IEmitterBase
    {
        public Vector3 SampleAt(float t);
    }
}