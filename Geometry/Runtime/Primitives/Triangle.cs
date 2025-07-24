using UnityEngine;

namespace EggCentric.Geometry.Primitives
{
    public struct Triangle : IPrimitiveData
    {
        public Vector2 Center { get; set; }
        public float Area { get; private set; }

        private Vector2[] points;

        public Triangle(Vector2 a, Vector2 b, Vector2 c)
        {
            Center = Vector2.zero;
            Area = 0f;
            points = new Vector2[] { a, b, c };

            CalculateCenter();
            LocalizePoints(ref points);
            CalculateArea();
        }

        private void CalculateCenter()
        {
            Center = Vector2.zero;

            foreach (var point in points)
            {
                Center += point;
            }
            Center /= points.Length;
        }

        private void CalculateArea()
        {
            Area = 0f;

            Vector2 aSide = points[1] - points[0];
            Vector2 bSide = points[2] - points[0];
            float angleBetween = Vector2.Angle(aSide, bSide) * Mathf.Deg2Rad;

            Area = 0.5f * aSide.magnitude * bSide.magnitude * Mathf.Sin(angleBetween);
        }

        private void LocalizePoints(ref Vector2[] worldPoints)
        {
            for (int i = 0; i < worldPoints.Length; i++)
            {
                worldPoints[i] = worldPoints[i] - Center;
            }
        }
    }
}