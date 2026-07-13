using UnityEngine;

namespace EggCentric.CoordinateSystems
{
    public abstract class CoordinateFrame : ICoordinateFrame
    {
        public bool IsLeftHanded { get; }
        public float Handedness { get; }

        public Vector3 MainDirection => _normal;
        public Vector3 SecondaryDirection => _tangent;
        public Vector3 TertiaryDirection => _bitangent;

        private readonly Vector3 _normal;
        private readonly Vector3 _tangent;
        private readonly Vector3 _bitangent;

        protected CoordinateFrame(Vector3 normal, Vector3 tangent, Vector3 bitangent)
        {
            _normal = normal.normalized;
            _tangent = tangent.normalized;
            _bitangent = bitangent.normalized;

            Handedness = ComputeHandednessSign();
            IsLeftHanded = Handedness < 0f;
        }

        public float ComputeHandednessSign() => Vector3.Dot(Vector3.Cross(SecondaryDirection, MainDirection), TertiaryDirection);
    }
}
