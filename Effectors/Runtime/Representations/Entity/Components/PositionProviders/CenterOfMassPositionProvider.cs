using UnityEngine;

namespace EggCentric.Effectors
{
    public class CenterOfMassPositionProvider : IEffectorPositionProvider
    {
        public Vector2 GetPosition(EffectorEntity effector)
        {
            if (effector.EffectZone && effector.EffectZone.attachedRigidbody)
                return effector.EffectZone.attachedRigidbody.worldCenterOfMass;

            return effector.transform.position;
        }
    }
}