using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EggCentric.ColliderExtensions;

namespace EggCentric.Geometry.Intersections
{
    public class ObjectIntersector : Intersector
    {
        public ObjectIntersector(Collider2D collider) : base(collider)
        {
        }

        public override Intersection GetIntersection(Collider2D otherCollider)
        {
            List<Vector2> intersectionPoints = new List<Vector2>();

            Vector2[] points = localBounds.LocalToPoints(collider.transform);

            bool[] isInternal = new bool[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                isInternal[i] = otherCollider.OverlapPoint(points[i]);
            }

            for (int i = 0, j = points.Length - 1; i < points.Length; j = i++)
            {
                if (isInternal[j])
                {
                    intersectionPoints.Add(points[j]);

                    if (isInternal[i])
                        continue;
                }

                var edgeStart = points[j];
                var edgeEnd = points[i];

                Vector2[] edgeIntersections = otherCollider.GetLineIntersections(edgeStart, edgeEnd).OrderBy(x => Vector2.Distance(edgeStart, x)).ToArray();
                intersectionPoints.AddRange(edgeIntersections);
            }

            return new Intersection(intersectionPoints);
        }
    }
}