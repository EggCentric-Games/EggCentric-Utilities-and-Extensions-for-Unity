using EggCentric.Sensors;
using UnityEngine;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityZoneComponent : MonoBehaviour
    {
        [SerializeField] private AreaSensor<IGravityAffectedBody> _contactDetector;
        [SerializeField] private IForceFieldSettings<IForceFieldContext> _settings;
        
        private GravityField _gravityZone;

        private void Awake()
        {
            IForceFieldContext context = _settings.CreateInstance();
            _gravityZone = new GravityField(context, _contactDetector);
        }
    }
}