using UnityEngine;

namespace EggCentric.Entities
{
    public interface IDirectionalEntity
    {
        public Vector3 Direction { get; }
        public Quaternion Rotation { get; }
    }
}