using UnityEngine;

public class CircleData : IPrimitiveData
{
    public Vector2 Center { get; set; }
    public float Radius { get; set; }

    public CircleData() : this(Vector2.zero, 0f)
    {
    }

    public CircleData(float radius) : this(Vector2.zero, radius)
    {
    }

    public CircleData(Vector2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }
}