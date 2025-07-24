using UnityEngine;
using EggCentric.ColliderExtensions;

namespace EggCentric.Geometry.Intersections
{
    public abstract class Intersector
    {
        public Collider2D Collider => collider;
        public Bounds LocalBounds => localBounds;

        protected Collider2D collider;
        protected Bounds localBounds;

        public Intersector(Collider2D collider)
        {
            this.collider = collider;
            this.localBounds = collider.GetLocalBounds();
        }

        public abstract Intersection GetIntersection(Collider2D otherCollider);
    }
}