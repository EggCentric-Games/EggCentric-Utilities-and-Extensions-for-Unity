using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class VerticalWorldFrame3D : CoordinateFrame
    {
        public VerticalWorldFrame3D() : base(Vector3.forward, Vector3.up, Vector3.right) { }
    }
}
