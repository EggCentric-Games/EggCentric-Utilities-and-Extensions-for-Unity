using System;
using UnityEngine;

namespace EggCentric.ColliderExtensions
{
    public static class ColliderExtensions
    {
        // Obsolete
        [Obsolete("Use collider.bounds.center instead")]
        public static Vector2 GetWorldPos(this Collider2D collider)
        {
            return collider.transform.position + (Vector3)collider.offset;
        }
    }
}