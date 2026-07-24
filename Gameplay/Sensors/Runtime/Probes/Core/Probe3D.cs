using UnityEngine;

namespace EggCentric.Sensors
{
    public abstract class Probe3D : CastingProbe<ProbeSettings3D, RaycastHit>
    {
        protected Probe3D(ProbeSettings3D settings) : base(settings)
        {
        }
    }
}
