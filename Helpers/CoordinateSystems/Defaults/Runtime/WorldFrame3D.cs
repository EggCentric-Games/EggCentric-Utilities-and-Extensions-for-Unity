using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class WorldFrame3D : CoordinateFrame
    {
        public WorldFrame3D() : base(Vector3.forward, Vector3.right, Vector3.up) { }
    }
}
