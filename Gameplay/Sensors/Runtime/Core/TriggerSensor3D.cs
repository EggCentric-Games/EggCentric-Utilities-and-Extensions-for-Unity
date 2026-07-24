using UnityEngine;

namespace EggCentric.Sensors
{
    public class TriggerSensor3D<TComponent> : TriggerSensor<TComponent, Collider>
    {
        private void OnTriggerEnter(Collider collider) => HandleTriggerEntry(collider);
        private void OnTriggerExit(Collider collider) => HandleTriggerExit(collider);
    }
}