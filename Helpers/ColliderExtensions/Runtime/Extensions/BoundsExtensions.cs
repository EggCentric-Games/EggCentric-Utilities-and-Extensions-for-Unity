using UnityEngine;

public static class BoundsExtensions
{
    public static Vector2[] ToPoints(this Bounds bounds)
    {
        Vector2 lowerLeft = new Vector2(bounds.min.x, bounds.min.y);
        Vector2 upperLeft = new Vector2(bounds.min.x, bounds.max.y);
        Vector2 lowerRight = new Vector2(bounds.max.x, bounds.min.y);
        Vector2 upperRight = new Vector2(bounds.max.x, bounds.max.y);

        Vector2[] points = new Vector2[] { lowerLeft, upperLeft, upperRight, lowerRight };

        return points;
    }

    public static Vector2[] LocalToPoints(this Bounds bounds, Transform parent)
    {
        Vector2[] points = ToPoints(bounds);
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = parent.TransformPoint(points[i]);
        }

        return points;
    }
}