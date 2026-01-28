using UnityEngine;

namespace EggCentric.Effectors
{
    public abstract class EffectorEntity : MonoBehaviour
    {
        public Vector2 Origin => _originProvider.Resolve(_parameters.ForceOrigin).GetPosition(this);
        public EffectorParameters Parameters => _parameters;
        public Collider2D EffectZone => _effectZone;

        [Header("References")]
        [SerializeField] protected Collider2D _effectZone;

        [SerializeField] private EffectorParameters _parameters;

        private IEffector _effector;
        private EffectorOriginProvider _originProvider = new EffectorOriginProvider();
        private EffectorSettingsHandler _settingsHandler;

        protected virtual void Initialize()
        {
            _parameters.Validate(out _);

            RefreshEffector();
        }

        protected virtual void SyncParameters() => _parameters.ResetToSerialized();

        protected void Awake() => Initialize();

        protected void Update() => ApplyEffects();

        private void OnValidate() => SyncParameters();

        private void RefreshEffector() => _effector = CreateEffector();

        private void RefreshSettings(EffectorSettings settings) => _effector.SetSettings(settings);

        private IEffector CreateEffector()
        {
            if (_effectZone && _parameters.ForceOrigin == ReferencePoint.ContactPoint)
                return new AreaEffector(_effectZone);

            return new PointEffector();
        }

        private void OnEnable()
        {
            _parameters.ForceOrigin.OnValueChangedNoArgs += RefreshEffector;

            _settingsHandler = new EffectorSettingsHandler(this);
            _settingsHandler.OnSettingsChanged += RefreshSettings;
        }

        private void OnDisable()
        {
            _parameters.ForceOrigin.OnValueChangedNoArgs -= RefreshEffector;

            _settingsHandler.Dispose();
            _settingsHandler = null;
        }

        protected abstract void ApplyEffects();
    }
}