using UnityEngine;

namespace EggCentric.Sensors
{
    public class RaycastProbe3D : Probe3D
    {
        public RaycastProbe3D(ProbeSettings3D settings) : base(settings)
        {
        }

        public override bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out RaycastHit hit)
        {
            var isHit = Physics.Raycast(origin, direction, out hit, maxDistance, Settings.Mask, Settings.TriggerInteraction);

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.red);
            if(isHit)
                Debug.DrawLine(origin, hit.point, Color.green);

            return isHit;
        }

        public override RaycastHit[] CastAll(Vector3 origin, Vector3 direction, float maxDistance)
        {
            var hits = Physics.RaycastAll(origin, direction, maxDistance, Settings.Mask, Settings.TriggerInteraction);

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.red);

            return hits;
        }
    }
}
