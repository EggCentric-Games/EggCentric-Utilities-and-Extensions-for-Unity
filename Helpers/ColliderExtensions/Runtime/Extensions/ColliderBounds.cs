using UnityEngine;

public static class ColliderBounds
{
    public static Bounds GetLocalBounds(this Collider2D collider)
    {
        switch (collider)
        {
            case BoxCollider2D:
                {
                    return ((BoxCollider2D)collider).GetLocalBounds();
                }
            case CapsuleCollider2D:
                {
                    return ((CapsuleCollider2D)collider).GetLocalBounds();
                }
            case CircleCollider2D:
                {
                    return ((CircleCollider2D)collider).GetLocalBounds();
                }
            case EdgeCollider2D:
                {
                    return ((EdgeCollider2D)collider).GetLocalBounds();
                }
            case PolygonCollider2D:
                {
                    return ((PolygonCollider2D)collider).GetLocalBounds();
                }
            default:
                {
                    Debug.LogError($"{collider} has unsupported type");
                    return new Bounds();
                }
        }
    }

    public static Bounds GetLocalBounds(this BoxCollider2D collider)
    {
        return new Bounds(collider.offset, collider.size);
    }

    public static Bounds GetLocalBounds(this CapsuleCollider2D collider)
    {
        return new Bounds(collider.offset, collider.size);
    }

    public static Bounds GetLocalBounds(this CircleCollider2D collider)
    {
        return new Bounds(collider.offset, Vector2.one * collider.radius * 2f);
    }

    public static Bounds GetLocalBounds(this EdgeCollider2D collider)
    {
        if (collider.points.Length == 0)
            return new Bounds();

        Vector2 minAnchor = collider.points[0];
        Vector2 maxAnchor = collider.points[0];

        for (int i = 1; i < collider.points.Length; i++)
        {
            Vector2 point = collider.points[i];
            if (point.x < minAnchor.x)
            {
                minAnchor.x = point.x;
            }
            if (point.y < minAnchor.y)
            {
                minAnchor.y = point.y;
            }
            if (point.x > maxAnchor.x)
            {
                maxAnchor.x = point.x;
            }
            if (point.y > maxAnchor.y)
            {
                maxAnchor.y = point.y;
            }
        }

        Vector2 center = Vector2.Lerp(minAnchor, maxAnchor, 0.5f);
        Vector2 size = maxAnchor - minAnchor;
        return new Bounds(center, size);
    }

    public static Bounds GetLocalBounds(this PolygonCollider2D collider)
    {
        if (collider.points.Length == 0)
            return new Bounds();

        Vector2 minAnchor = collider.points[0];
        Vector2 maxAnchor = collider.points[0];

        for (int i = 1; i < collider.points.Length; i++)
        {
            Vector2 point = collider.points[i];
            if (point.x < minAnchor.x)
            {
                minAnchor.x = point.x;
            }
            if (point.y < minAnchor.y)
            {
                minAnchor.y = point.y;
            }
            if (point.x > maxAnchor.x)
            {
                maxAnchor.x = point.x;
            }
            if (point.y > maxAnchor.y)
            {
                maxAnchor.y = point.y;
            }
        }

        Vector2 center = Vector2.Lerp(minAnchor, maxAnchor, 0.5f);
        Vector2 size = maxAnchor - minAnchor;
        return new Bounds(center, size);
    }
}
