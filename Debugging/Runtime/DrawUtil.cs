using UnityEngine;
using EggCentric.Geometry;

namespace EggCentric.Debugging
{
    public static class DrawUtil
    {
        public static void DrawCross(Vector3 point, float size, Color color)
        {
            DrawAxis(point, Vector3.up, size, color);
            DrawAxis(point, Vector3.right, size, color);
            DrawAxis(point, Vector3.forward, size, color);
        }

        public static void DrawAxis(Vector3 point, Vector3 axis, float size, Color color)
        {
            float extent = size / 2f;
            Vector3 beam = axis * extent;
            Debug.DrawLine(point - beam, point + beam, color);
        }

        public static void DrawCircle(Vector3 position, Vector3 normal, float radius, int precision = 16, Color? color = null)
        {
            Color drawColor = color.HasValue ? color.Value : Color.green;

            Vector3[] circle = PointPlacementUtil.GetCircle(normal, radius, precision);

            for (int i = 0; i < circle.Length; ++i)
            {
                Vector3 segmentStart = circle[i];
                Vector3 segmentEnd;

                if (i != circle.Length - 1)
                    segmentEnd = circle[i + 1];
                else
                    segmentEnd = circle[0];

                Debug.DrawLine(position + segmentStart, position + segmentEnd, drawColor);
            }
        }

        public static void DrawCylinder(Vector3 position, Vector3 normal, float height, float radius, int precision = 16, Color? color = null)
        {
            Color drawColor = color.HasValue ? color.Value : Color.green;

            Vector3[] circle = PointPlacementUtil.GetCircle(normal, radius, precision);
            Vector3 depthOffset = normal * (height / 2f);

            DrawCircle(position + depthOffset, normal, radius, precision, drawColor);
            DrawCircle(position - depthOffset, normal, radius, precision, drawColor);
            for (int i = 0; i < circle.Length; ++i)
            {
                Vector3 segmentStart = circle[i] + depthOffset;
                Vector3 segmentEnd = circle[i] - depthOffset;

                Debug.DrawLine(position + segmentStart, position + segmentEnd, drawColor);
            }
        }
    }
}