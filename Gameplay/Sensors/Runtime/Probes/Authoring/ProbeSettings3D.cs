using UnityEngine;

namespace EggCentric.Sensors
{
    [System.Serializable]
    public struct ProbeSettings3D
    {
        public LayerMask Mask;
        public QueryTriggerInteraction TriggerInteraction;
    }
}
