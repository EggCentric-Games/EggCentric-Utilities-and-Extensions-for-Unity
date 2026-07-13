using UnityEngine;

namespace EggCentric.CoordinateSystems.Defaults
{
    public sealed class VerticalWorldFrame2D : CoordinateFrame
    {
        public VerticalWorldFrame2D() : base(Vector3.up, Vector3.right, Vector3.forward) { }
    }
}
