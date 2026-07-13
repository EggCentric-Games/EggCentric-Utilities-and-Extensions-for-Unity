namespace EggCentric.CoordinateSystems.Defaults
{
    public static class CoordinateFrames
    {
        public static readonly ICoordinateFrame HorizontalFrame2D = new HorizontalWorldFrame2D();
        public static readonly ICoordinateFrame VerticalFrame2D = new VerticalWorldFrame2D();
        public static readonly ICoordinateFrame HorizontalFrame3D = new HorizontalWorldFrame3D();
        public static readonly ICoordinateFrame VerticalFrame3D = new VerticalWorldFrame3D();
    }
}
