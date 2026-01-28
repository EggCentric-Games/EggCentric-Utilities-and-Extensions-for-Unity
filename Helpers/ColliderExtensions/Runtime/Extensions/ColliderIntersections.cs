using EggCentric.Geometry;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderIntersections
{
    public static Vector2[] GetLineIntersections(this Collider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        switch (collider)
        {
            case BoxCollider2D:
                {
                    return ((BoxCollider2D)collider).GetLineIntersections(lineStart, lineEnd);
                }
            case CapsuleCollider2D:
                {
                    return ((CapsuleCollider2D)collider).GetLineIntersections(lineStart, lineEnd);
                }
            case CircleCollider2D:
                {
                    return ((CircleCollider2D)collider).GetLineIntersections(lineStart, lineEnd);
                }
            case EdgeCollider2D:
                {
                    return ((EdgeCollider2D)collider).GetLineIntersections(lineStart, lineEnd);
                }
            case PolygonCollider2D:
                {
                    return ((PolygonCollider2D)collider).GetLineIntersections(lineStart, lineEnd);
                }
            default:
                {
                    Debug.LogError($"{collider} has unsupported type");
                    return new Vector2[0];
                }
        }
    }

    public static Vector2[] GetLineIntersections(this BoxCollider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2[] polygonPoints = collider.GetLocalBounds().LocalToPoints(collider.transform);

        return GeometryOperations.GetConvexPolygonIntersection(polygonPoints, lineStart, lineEnd);
    }

    public static Vector2[] GetLineIntersections(this CapsuleCollider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2 extents = collider.size / 2f;
        bool isHorizontal = collider.direction == CapsuleDirection2D.Horizontal;
        float capsuleRadius = isHorizontal ? extents.y : extents.x;

        bool isCircle = (isHorizontal && collider.size.x <= collider.size.y) || (!isHorizontal && collider.size.y <= collider.size.x);
        if (isCircle)
            return GeometryOperations.GetCircleIntersection(collider.transform.position, capsuleRadius, lineStart, lineEnd);

        List<Vector2> intersectionPoints = new List<Vector2>();

        Vector2 capsuleDirection = isHorizontal ? collider.transform.right : collider.transform.up;
        Vector2 capsuleNormal = isHorizontal ? collider.transform.up : collider.transform.right;
        float capsuleHeight = Mathf.Max(collider.size.x, collider.size.y);
        float capsuleWidth = Mathf.Min(collider.size.x, collider.size.y);
        Vector2 capsuleDimensions = new Vector2(capsuleWidth / 2f, capsuleHeight / 2f);

        float distanceToAnchor = capsuleDimensions.y - capsuleRadius;
        Vector2 center = (Vector2)collider.transform.position + collider.offset;
        Vector2 firstAnchor = center + capsuleDirection * distanceToAnchor;
        Vector2 secondAnchor = center - capsuleDirection * distanceToAnchor;
        Vector2[] anchors = new Vector2[] { firstAnchor, secondAnchor };

        Vector2 directionToSide = capsuleNormal * capsuleDimensions.x;
        Vector2[] leftBodyLine = new Vector2[] { firstAnchor - directionToSide, secondAnchor - directionToSide };
        Vector2[] rightBodyLine = new Vector2[] { firstAnchor + directionToSide, secondAnchor + directionToSide };

        if (GeometryOperations.LineSegmentIntersection(leftBodyLine[0], leftBodyLine[1], lineStart, lineEnd, out Vector2 leftSideIntersectionPoint))
            intersectionPoints.Add(leftSideIntersectionPoint);

        if (GeometryOperations.LineSegmentIntersection(rightBodyLine[0], rightBodyLine[1], lineStart, lineEnd, out Vector2 rightSideIntersectionPoint))
            intersectionPoints.Add(rightSideIntersectionPoint);

        if (intersectionPoints.Count == 2)
            return intersectionPoints.ToArray();

        Vector2 firstAnchorClosest = GeometryOperations.ClosestPointOnLineSegment(lineStart, lineEnd, anchors[0]);
        float distanceToFirstAnchor = (firstAnchorClosest - anchors[0]).magnitude;
        if (distanceToFirstAnchor <= capsuleRadius)
        {
            Vector2[] intersections = GeometryOperations.GetCircleIntersection(anchors[0], capsuleRadius, lineStart, lineEnd);

            if (intersections.Length == 1)
            {
                intersectionPoints.Add(intersections[0]);
                return intersectionPoints.ToArray();
            }

            if (intersections.Length == 2)
            {
                bool isOutside = GeometryOperations.SignedTriangleArea(leftBodyLine[0], rightBodyLine[0], intersections[0]) > 0;
                if (isOutside)
                {
                    intersectionPoints.Add(intersections[0]);

                    if (intersectionPoints.Count == 2)
                        return intersectionPoints.ToArray();
                }

                isOutside = GeometryOperations.SignedTriangleArea(leftBodyLine[0], rightBodyLine[0], intersections[1]) > 0;
                if (isOutside)
                {
                    intersectionPoints.Add(intersections[1]);

                    if (intersectionPoints.Count == 2)
                        return intersectionPoints.ToArray();
                }
            }
        }

        Vector2 secondAnchorClosest = GeometryOperations.ClosestPointOnLineSegment(lineStart, lineEnd, anchors[0]);
        float distanceTosecondAnchor = (secondAnchorClosest - anchors[0]).magnitude;
        if (distanceTosecondAnchor <= capsuleRadius)
        {
            Vector2[] intersections = GeometryOperations.GetCircleIntersection(anchors[1], capsuleRadius, lineStart, lineEnd);

            if (intersections.Length == 1)
            {
                intersectionPoints.Add(intersections[0]);
                return intersectionPoints.ToArray();
            }

            if (intersections.Length == 2)
            {
                bool isOutside = GeometryOperations.SignedTriangleArea(leftBodyLine[1], rightBodyLine[1], intersections[0]) > 0;
                if (isOutside)
                {
                    intersectionPoints.Add(intersections[0]);

                    if (intersectionPoints.Count == 2)
                        return intersectionPoints.ToArray();
                }

                isOutside = GeometryOperations.SignedTriangleArea(leftBodyLine[1], rightBodyLine[1], intersections[1]) > 0;
                if (isOutside)
                {
                    intersectionPoints.Add(intersections[1]);

                    if (intersectionPoints.Count == 2)
                        return intersectionPoints.ToArray();
                }
            }
        }

        return intersectionPoints.ToArray();
    }

    public static Vector2[] GetLineIntersections(this CircleCollider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        return GeometryOperations.GetCircleIntersection(collider.transform.position, collider.radius, lineStart, lineEnd);
    }

    public static Vector2[] GetLineIntersections(this EdgeCollider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        List<Vector2> intersectionPoints = new List<Vector2>();
        Vector2[] edgePoints = collider.points;

        for (int i = 0; i < edgePoints.Length; i++)
            edgePoints[i] = collider.transform.TransformPoint(edgePoints[i]);

        for (int i = 1, j = 0; i < edgePoints.Length; j = i++)
            if (GeometryOperations.LineSegmentIntersection(lineStart, lineEnd, edgePoints[i], edgePoints[j], out Vector2 intersectionPoint))
                intersectionPoints.Add(intersectionPoint);

        return intersectionPoints.ToArray();
    }

    public static Vector2[] GetLineIntersections(this PolygonCollider2D collider, Vector2 lineStart, Vector2 lineEnd)
    {
        Vector2[] polygonPoints = collider.points;

        for (int i = 0; i < polygonPoints.Length; i++)
            polygonPoints[i] = collider.transform.TransformPoint(polygonPoints[i]);

        return GeometryOperations.GetConvexPolygonIntersection(polygonPoints, lineStart, lineEnd);
    }
}