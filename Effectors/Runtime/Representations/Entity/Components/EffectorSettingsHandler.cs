using EggCentric.Evaluators;
using EggCentric.Evaluators.EggCentric.Evaluators;
using System;
using UnityEngine;

namespace EggCentric.Effectors
{
    public class EffectorSettingsHandler
    {
        private Vector3 _referenceDirection => Quaternion.AngleAxis(_effectorEntity.Parameters.ReferenceAngle, Vector3.forward) * Vector2.right;

        private readonly EffectorEntity _effectorEntity;

        public event Action<EffectorSettings> OnSettingsChanged;

        public EffectorSettingsHandler(EffectorEntity effectorEntity)
        {
            _effectorEntity = effectorEntity;

            SubscribeToEvents();
        }

        public void Dispose() => UnsubscribeFromEvents();

        private void UpdateParameters() => OnSettingsChanged?.Invoke(CreateParameters());

        private EffectorSettings CreateParameters()
        {
            var settings = new EffectorSettings();

            settings.Origin = _effectorEntity.Origin;
            settings.ReferenceDirection = _referenceDirection;
            settings.Growth = GrowthFactory.Create(_effectorEntity.Parameters.GrowthType, _effectorEntity.Parameters.GrowthBase);
            settings.Shaping = ShapingFactory.Create(_effectorEntity.Parameters.ShapingType);
            settings.BaseValue = _effectorEntity.Parameters.BaseValue;
            settings.ValueLimit = _effectorEntity.Parameters.ValueLimit;

            return settings;
        }

        private void SubscribeToEvents()
        {
            _effectorEntity.Parameters.ForceOrigin.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.ReferenceAngle.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.GrowthType.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.GrowthBase.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.ShapingType.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.BaseValue.OnValueChangedNoArgs += UpdateParameters;
            _effectorEntity.Parameters.ValueLimit.OnValueChangedNoArgs += UpdateParameters;
        }

        private void UnsubscribeFromEvents()
        {
            _effectorEntity.Parameters.ForceOrigin.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.ReferenceAngle.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.GrowthType.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.GrowthBase.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.ShapingType.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.BaseValue.OnValueChangedNoArgs -= UpdateParameters;
            _effectorEntity.Parameters.ValueLimit.OnValueChangedNoArgs -= UpdateParameters;
        }
    }
}