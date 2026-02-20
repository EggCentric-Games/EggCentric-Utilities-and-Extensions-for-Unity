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

        public static Vector2 Project(this Vector2 source, Vector2 target)
        {
            Vector2 targetDirection = target.normalized;
            float t = Vector2.Dot(source, targetDirection);
            Vector2 projection = targetDirection * t;

            return projection;
        }

        public static float DistanceToLine(this Vector2 source, Vector2 target) => Vector2.Dot(source, target.Normal());

        public static Vector2 FromAngle(float angle) => new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        public static Vector2 Normal(this Vector2 source, bool isInverted = false)
        {
            source.Normalize();

            if(isInverted)
                return new Vector2(source.y, -source.x);

            return new Vector2(-source.y, source.x);
        }
    }
}