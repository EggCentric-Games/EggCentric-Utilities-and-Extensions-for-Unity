using System.Collections.Generic;
using UnityEngine;
using EggCentric.Geometry.Primitives;

namespace EggCentric.Geometry
{
    public static class GeometryOperations
    {
        public static Triangle[] TriangulateConvexPolygon(Vector2[] points)
        {
            if (points.Length < 3)
                return new Triangle[0];

            Triangle[] triangles = new Triangle[points.Length - 2];

            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i] = new Triangle(points[0], points[i + 1], points[i + 2]);
            }
            return triangles;
        }

        public static Vector2 GetCenter(Triangle[] triangles)
        {
            Vector2 center = Vector2.zero;

            foreach (Triangle triangle in triangles)
            {
                center += triangle.Center;
            }
            center /= triangles.Length;

            return center;
        }

        public static float GetArea(Triangle[] triangles)
        {
            float area = 0f;

            foreach (Triangle triangle in triangles)
            {
                area += triangle.Area;
            }

            return area;
        }

        public static Vector2[] GetConvexPolygonIntersection(Vector2[] points, Vector2 lineStart, Vector2 lineEnd)
        {
            List<Vector2> intersectionPoints = new List<Vector2>();

            var pointCount = points.Length;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i++)
            {
                var edgeStart = points[i];
                var edgeEnd = points[j];

                Vector2 clipPoint;
                if (LineSegmentIntersection(lineStart, lineEnd, edgeStart, edgeEnd, out clipPoint))
                    intersectionPoints.Add(clipPoint);

                if (intersectionPoints.Count == 2)
                    break;
            }

            return intersectionPoints.ToArray();
        }

        public static Vector2[] GetCircleIntersection(Vector2 center, float radius, Vector2 lineStart, Vector2 lineEnd)
        {
            List<Vector2> intersections = new List<Vector2>();

            Vector2 direction = lineEnd - lineStart;
            Vector2 relativeStart = lineStart - center;

            float A = Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2);
            float B = 2 * ((direction.x * relativeStart.x) + (direction.y * relativeStart.y));
            float C = Mathf.Pow(relativeStart.x, 2) + Mathf.Pow(relativeStart.y, 2) - Mathf.Pow(radius, 2);

            float det = Mathf.Pow(B, 2) - 4 * A * C;

            if (det == 0)
            {
                float t = -B / (2 * A);
                if (HandleRange(lineStart, direction, t, out Vector2 intersectionPoint))
                {
                    intersections.Add(intersectionPoint);
                }
            }
            else if (det > 0)
            {
                float t1 = (float)((-B + Mathf.Sqrt(det)) / (2 * A));
                if (HandleRange(lineStart, direction, t1, out Vector2 intersectionPoint))
                {
                    intersections.Add(intersectionPoint);
                }

                float t2 = (float)((-B - Mathf.Sqrt(det)) / (2 * A));
                if (HandleRange(lineStart, direction, t2, out intersectionPoint))
                {
                    intersections.Add(intersectionPoint);
                }
            }

            return intersections.ToArray();
        }

        public static bool LineSegmentIntersection(Vector2 a, Vector2 b, Vector2 c, Vector2 d, out Vector2 point)
        {
            // Sign of areas correspond to which side of ab points c and d are.
            float area1 = SignedTriangleArea(a, b, d);
            float area2 = SignedTriangleArea(a, b, c);

            // If c and d are on different sides of ab, areas have different signs.
            if (area1 * area2 < 0.0f)
            {
                // Compute signs for a and b with respect to segment cd.
                float area3 = SignedTriangleArea(c, d, a);
                float area4 = area3 + area2 - area1;

                // Points a and b on different sides of cd if areas have different signs.
                if (area3 * area4 < 0.0f)
                {
                    float time = area3 / (area3 - area4);
                    point = a + time * (b - a);
                    return true;
                }
            }

            // Segments are not intersecting or collinear.
            point = Vector2.zero;
            return false;
        }

        public static Vector3 ClosestPointOnLineSegment(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
        {
            Vector3 line = lineEnd - lineStart;
            float t = Vector3.Dot(point - lineStart, line) / Vector3.Dot(line, line);
            return lineStart + Mathf.Clamp01(t) * line;
        }

        public static Vector2 GetArithmeticCenter(Vector2[] points)
        {
            Vector2 center = Vector2.zero;

            for (int i = 0; i < points.Length; i++)
            {
                center += points[i];
            }
            center /= points.Length;

            return center;
        }

        public static float SignedTriangleArea(Vector2 a, Vector2 b, Vector2 c)
        {
            return (a.x - c.x) * (b.y - c.y) - (a.y - c.y) * (b.x - c.x);
        }

        private static bool HandleRange(Vector2 start, Vector2 direction, float t, out Vector2 intersection)
        {
            intersection = Vector2.zero;
            if (t < 0 || t > 1)
                return false;

            intersection = start + direction * t;
            return true;
        }
    }
}
