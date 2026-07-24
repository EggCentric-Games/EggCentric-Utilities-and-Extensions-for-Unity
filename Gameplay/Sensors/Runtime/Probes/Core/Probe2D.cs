using UnityEngine;

namespace EggCentric.Sensors
{
    public abstract class Probe2D : CastingProbe<ProbeSettings2D, RaycastHit2D>
    {
        protected Probe2D(ProbeSettings2D settings) : base(settings)
        {
        }
    }
}
