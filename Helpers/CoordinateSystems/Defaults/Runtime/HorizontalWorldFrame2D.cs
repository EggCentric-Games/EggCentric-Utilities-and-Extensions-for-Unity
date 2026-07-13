using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class HorizontalWorldFrame2D : CoordinateFrame
    {
        public HorizontalWorldFrame2D() : base(Vector3.right, Vector3.up, Vector3.forward) { }
    }
}
