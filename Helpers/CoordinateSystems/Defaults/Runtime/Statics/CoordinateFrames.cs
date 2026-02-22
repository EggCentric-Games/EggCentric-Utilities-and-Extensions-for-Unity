namespace EggCentric.CoordinateSystems.Defaults
{
    public static class CoordinateFrames
    {
        public static readonly ICoordinateFrame Frame2D = new WorldFrame2D();
        public static readonly ICoordinateFrame Frame3D = new WorldFrame3D();
    }
}
