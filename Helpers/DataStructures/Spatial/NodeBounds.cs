using UnityEngine;

namespace EggCentric.DataStructures
{
    [System.Serializable]
    public class NodeBounds
    {
        public readonly Vector3 Center;
        public readonly Vector3 Size;

        public readonly Vector3 Extents;
        public readonly Vector3 Min;
        public readonly Vector3 Max;

        public NodeBounds(Vector3 center, Vector3 extents)
        {
            Center = center;
            Extents = extents;

            Size = extents * 2f;
            Min = center - Extents;
            Max = center + Extents;
        }

        public float SqrDistance(Vector3 position)
        {
            float dx = 0f;
            if (position.x < Min.x)
                dx = Min.x - position.x;
            else if (position.x > Max.x)
                dx = position.x - Max.x;

            float dy = 0f;
            if (position.y < Min.y)
                dy = Min.y - position.y;
            else if (position.y > Max.y)
                dy = position.y - Max.y;

            float dz = 0f;
            if (position.z < Min.z)
                dz = Min.z - position.z;
            else if (position.z > Max.z)
                dz = position.z - Max.z;

            return dx * dx + dy * dy + dz * dz;
        }

        public bool Contains(Vector3 position) => position.x >= Min.x && position.x <= Max.x && position.y >= Min.y && position.y <= Max.y && position.z >= Min.z && position.z <= Max.z;
    }
}