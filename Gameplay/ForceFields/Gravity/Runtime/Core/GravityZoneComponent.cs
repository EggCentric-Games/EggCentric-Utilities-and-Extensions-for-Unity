using EggCentric.Sensors;
using UnityEngine;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityZoneComponent : MonoBehaviour
    {
        [SerializeField] private TriggerSensor2D<IGravityAffectedBody> _contactDetector;
        [SerializeField] private IGravityFieldSettings<IGravityFieldContext> _settings;
        
        private GravityField _gravityZone;

        private void Awake()
        {
            var context = _settings.CreateInstance();
            _gravityZone = new GravityField(context, _contactDetector);
        }
    }
}