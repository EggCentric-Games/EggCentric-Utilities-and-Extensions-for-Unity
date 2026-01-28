using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EggCentric.ColliderExtensions;

namespace EggCentric.Geometry.Intersections
{
    public class EdgeIntersector : Intersector
    {
        public EdgeIntersector(Collider2D collider) : base(collider)
        {
        }

        public override Intersection GetIntersection(Collider2D otherCollider)
        {
            List<Vector2> intersectionPoints = new List<Vector2>();

            Vector2[] points = localBounds.LocalToPoints(collider.transform);

            for (int i = 0, j = points.Length - 1; i < points.Length; j = i++)
            {
                var edgeStart = points[j];
                var edgeEnd = points[i];

                Vector2[] edgeIntersections = otherCollider.GetLineIntersections(edgeStart, edgeEnd).OrderBy(x => Vector2.Distance(edgeStart, x)).ToArray();
                intersectionPoints.AddRange(edgeIntersections);
            }

            return new Intersection(intersectionPoints);
        }
    }
}