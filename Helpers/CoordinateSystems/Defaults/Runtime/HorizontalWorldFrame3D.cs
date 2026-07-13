using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class HorizontalWorldFrame3D : CoordinateFrame
    {
        public HorizontalWorldFrame3D() : base(Vector3.forward, Vector3.right, Vector3.up) { }
    }
}
