using UnityEngine;

namespace EggCentric.Emmiters
{
    public struct Point
    {
        public Vector3 Position;
        public Vector3 Direction;

        public Point(Vector3 position, Vector3 direction)
        {
            Position = position;
            Direction = direction;
        }

        public static Point operator *(Quaternion rotation, Point point) {
            point.Position = rotation * point.Position;
            point.Direction = rotation * point.Direction;

            return point;
        }
    }
}