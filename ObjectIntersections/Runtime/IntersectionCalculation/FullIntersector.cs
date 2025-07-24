using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using EggCentric.ColliderExtensions;

namespace EggCentric.Geometry.Intersections
{
    public class FullIntersector : Intersector
    {
        public FullIntersector(Collider2D collider) : base(collider)
        {
        }

        public override Intersection GetIntersection(Collider2D otherCollider)
        {
            Debug.LogError($"Full-intersections currently not working correctly!");

            List<Vector2> intersectionPoints = new List<Vector2>();

            Vector2[] selfAnchors = localBounds.LocalToPoints(collider.transform);
            Vector2[] otherAnchors = otherCollider.GetLocalBounds().LocalToPoints(otherCollider.transform);

            bool[] isInternal = new bool[selfAnchors.Length];
            for (int i = 0; i < selfAnchors.Length; i++)
            {
                isInternal[i] = otherCollider.OverlapPoint(selfAnchors[i]);
            }

            for (int i = 0, j = selfAnchors.Length - 1; i < selfAnchors.Length; j = i++)
            {
                if (isInternal[j])
                {
                    intersectionPoints.Add(selfAnchors[j]);

                    if (isInternal[i])
                        continue;
                }

                var edgeStart = selfAnchors[j];
                var edgeEnd = selfAnchors[i];

                Vector2[] edgeIntersections = otherCollider.GetLineIntersections(edgeStart, edgeEnd).OrderBy(x => Vector2.Distance(edgeStart, x)).ToArray();
                intersectionPoints.AddRange(edgeIntersections);
            }

            for (int i = 0; i < otherAnchors.Length; i++)
            {
                if (collider.OverlapPoint(otherAnchors[i]))
                    intersectionPoints.Add(otherAnchors[i]);
            }

            return new Intersection(intersectionPoints);
        }
    }
}