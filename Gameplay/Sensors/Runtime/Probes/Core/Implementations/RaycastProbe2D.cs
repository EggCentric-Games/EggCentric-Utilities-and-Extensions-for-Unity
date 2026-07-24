using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.Sensors
{
    public class RaycastProbe2D : Probe2D
    {
        private readonly List<RaycastHit2D> _hits = new();
        private readonly RaycastHit2D[] _hit = new RaycastHit2D[1];

        public RaycastProbe2D(ProbeSettings2D settings) : base(settings)
        {
        }

        public override bool Cast(Vector3 origin, Vector3 direction, float maxDistance, out RaycastHit2D hit)
        {
            var isHit = Physics2D.Raycast(origin, direction, Settings.ContactFilter, _hit, maxDistance) > 0;
            hit = _hit[0];

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.red);
            if (isHit)
                Debug.DrawLine(origin, hit.point, Color.green);

            return isHit;
        }

        public override RaycastHit2D[] CastAll(Vector3 origin, Vector3 direction, float maxDistance)
        {
            _hits.Clear();
            var isHit = Physics2D.Raycast(origin, direction, Settings.ContactFilter, _hits, maxDistance);

            Debug.DrawLine(origin, origin + direction * maxDistance, Color.red);

            return _hits.ToArray();
        }
    }
}
