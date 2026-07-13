using UnityEngine;

namespace EggCentric.Sensors
{
    public class TriggerSensor2D<TComponent> : TriggerSensor<TComponent, Collider2D> where TComponent : Component
    {
        private void OnTriggerEnter2D(Collider2D collider) => HandleTriggerEntry(collider);
        private void OnTriggerExit2D(Collider2D collider) => HandleTriggerExit(collider);
    }
}