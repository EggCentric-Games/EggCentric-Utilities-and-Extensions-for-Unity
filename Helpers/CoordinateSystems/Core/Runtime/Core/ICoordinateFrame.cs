using UnityEngine;

namespace EggCentric.CoordinateSystems
{
    public interface ICoordinateFrame
    {
        public Vector3 Normal { get; }
        public Vector3 Tangent { get; }
        public Vector3 Bitangent { get; }

        public bool IsLeftHanded { get; }
        public float Handedness { get; }
    }
}
