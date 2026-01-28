using System.Collections.Generic;
using UnityEngine;
using EggCentric.Geometry.Primitives;

namespace EggCentric.Geometry.Intersections
{
    public struct Intersection
    {
        public Vector2[] Points { get; private set; }
        public Vector2 Center { get; private set; }
        public float Area { get; private set; }

        private Triangle[] triangles;

        public Intersection(Vector2[] points)
        {
            Points = points;
            triangles = GeometryOperations.TriangulateConvexPolygon(points);

            bool triangulated = points.Length >= 3;
            Center = triangulated ? GeometryOperations.GetCenter(triangles) : GeometryOperations.GetArithmeticCenter(points);
            Area = triangulated ? GeometryOperations.GetArea(triangles) : 0f;
        }

        public Intersection(List<Vector2> points) : this(points.ToArray())
        {
        }
    }
}