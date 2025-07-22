using UnityEngine;

namespace EggCentric.QoL
{
    public static class ConvertingExtensions
    {
        public static Vector2[] ToVector2Array(this Vector3[] vector3Array)
        {
            return System.Array.ConvertAll(vector3Array, ToVector2);
        }

        public static Vector2 ToVector2(this Vector3 vector3)
        {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector3[] ToVector3Array(this Vector2[] vector3Array)
        {
            return System.Array.ConvertAll(vector3Array, ToVector3);
        }

        public static Vector3 ToVector3(this Vector2 vector2)
        {
            return new Vector3(vector2.x, vector2.y, 0f);
        }

        public static Vector3 ToVector3XZ(this Vector2 vector2)
        {
            return new Vector3(vector2.x, 0f, vector2.y);
        }
    }
}