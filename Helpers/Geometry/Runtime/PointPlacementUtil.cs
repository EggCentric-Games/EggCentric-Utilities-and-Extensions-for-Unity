using UnityEngine;

namespace EggCentric.Geometry
{
    public static class PointPlacementUtil
    {
        public static Vector3[] GetLine(Vector3 direction, float length, int subdivisions)
        {
            if (subdivisions < 0)
            {
                Debug.LogWarning($"Subdivision should not be less than 0!");
                subdivisions = 0;
            }

            int pointCount = subdivisions + 2;
            float offset = length / (pointCount - 1);
            Vector3[] points = PlacePointsOnLine(direction, length, pointCount, offset);

            return points;
        }

        public static Vector3[] GetLine(Vector3 direction, int pointCount, float offset = 1f)
        {
            if (pointCount < 1)
            {
                Debug.LogError($"Point count could not be less than 1!");
                return null;
            }

            float totalLength = offset * (pointCount - 1);
            Vector3[] points = PlacePointsOnLine(direction, totalLength, pointCount, offset);

            return points;
        }

        public static Vector3[,] GetGrid(Vector2Int resolution, Vector2? size)
        {
            Vector2 gridSize = size.HasValue ? size.Value : Vector2.one;
            Vector3[,] points = new Vector3[resolution.x, resolution.y];

            Vector2 startingPoint = gridSize / 2f * -1f;
            Vector2 step = new Vector2(gridSize.x / (resolution.x - 1), gridSize.y / (resolution.y - 1));
            for (int i = 0; i < resolution.y; i++)
            {
                for (int j = 0; j < resolution.x; j++)
                {
                    points[j, i] = new Vector3(startingPoint.x + (step.x * j), startingPoint.y + (step.y * i));
                }
            }

            return points;
        }

        public static Vector3[] GetCircle(Vector3 normal, float radius, int roundness = 16)
        {
            Vector3[] points = new Vector3[roundness];
            Quaternion facingRotation = Quaternion.FromToRotation(Vector3.forward, normal);

            float step = 360f / roundness;
            Quaternion stepRotation = Quaternion.AngleAxis(step, normal);

            Vector3 point = facingRotation * Vector3.right * radius;
            for (int i = 0; i < roundness; ++i)
            {
                points[i] = point;
                point = stepRotation * point;
            }

            return points;
        }

        public static Vector3[] GetCylinder(Vector3 normal, float height, float radius, int precision = 16)
        {
            Vector3[] cylinder = new Vector3[precision * 2];

            Vector3[] circle = GetCircle(normal, radius, precision);
            Vector3 depthOffset = normal * (height / 2f);

            for (int i = 0; i < precision; i++)
            {
                cylinder[i] = circle[i] - depthOffset;
                cylinder[i + precision] = circle[i] + depthOffset;
            }

            return cylinder;
        }

        private static Vector3[] PlacePointsOnLine(Vector3 direction, float length, int pointCount, float offset)
        {
            if (pointCount < 1)
            {
                Debug.LogError($"Point count could not be less than 1!");
                return null;
            }

            Vector3[] points = new Vector3[pointCount];

            Vector3 step = direction.normalized * offset;
            Vector3 firstPoint = direction.normalized * (length / 2f) * -1f;

            for (int i = 0; i < pointCount; i++)
                points[i] = firstPoint + step * i;

            return points;
        }
    }
}