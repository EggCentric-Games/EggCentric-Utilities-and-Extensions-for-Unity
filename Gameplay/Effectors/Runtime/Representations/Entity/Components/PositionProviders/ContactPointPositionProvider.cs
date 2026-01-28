using UnityEngine;

namespace EggCentric.Effectors
{
    public class ContactPointPositionProvider : IEffectorPositionProvider
    {
        public Vector2 GetPosition(EffectorEntity effector)
        {
            if (effector.EffectZone)
                return effector.EffectZone.bounds.center;

            return effector.transform.position;
        }
    }
}