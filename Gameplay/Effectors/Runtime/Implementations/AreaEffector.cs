using UnityEngine;

namespace EggCentric.Effectors
{
    public class AreaEffector : Effector
    {
        protected Collider2D effectZone;

        public AreaEffector(Collider2D effectZone)
        {
            this.effectZone = effectZone;
        }

        public override Vector2 EvaluateAt(Vector2 position)
        {
            Vector2 closestPoint = effectZone.ClosestPoint(position);

            return base.EvaluateAt(closestPoint);
        }
    }
}