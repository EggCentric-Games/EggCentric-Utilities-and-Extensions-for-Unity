using UnityEngine;

namespace EggCentric.QoL
{
    public static class VectorExtensions
    {
        public static Vector3 GetTangent(this Vector3 value)
        {
            value.Normalize();

            Vector3 upDirection = value != Vector3.up ? Vector3.up : Vector3.right;
            Vector3 tangent = Vector3.Cross(value, upDirection);
            tangent.Normalize();

            return tangent;
        }

        public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, float rotation)
        {
            var direction = point - pivot;
            var rotatedDirection = Quaternion.Euler(rotation * Vector3.right) * direction;

            return pivot + rotatedDirection;
        }
    }
}