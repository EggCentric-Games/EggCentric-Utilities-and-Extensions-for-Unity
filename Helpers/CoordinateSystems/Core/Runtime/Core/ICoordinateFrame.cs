using UnityEngine;

namespace EggCentric.CoordinateSystems
{
    public interface ICoordinateFrame
    {
        public Vector3 MainDirection { get; }
        public Vector3 SecondaryDirection { get; }
        public Vector3 TertiaryDirection { get; }
        
        public bool IsLeftHanded { get; }
        public float Handedness { get; }
    }
}
