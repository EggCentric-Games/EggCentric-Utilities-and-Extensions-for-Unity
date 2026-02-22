using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class WorldFrame2D : CoordinateFrame
    {
        public WorldFrame2D() : base(Vector3.right, Vector3.forward, Vector3.up) { }
    }
}
