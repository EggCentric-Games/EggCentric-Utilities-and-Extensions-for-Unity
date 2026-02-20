using System;
using UnityEngine;

namespace EggCentric.ValueProviders.Stats
{
    public class ValueStat : IStat
    {
        protected float currentValue;

        public float Value => currentValue;

        public event Action<float> OnValueChanged;
        public event Action OnValueChangedNoArgs;

        public ValueStat(float value = 0f)
        {
            this.currentValue = value;
        }

        public virtual void SetValue(float value)
        {
            float change = value - Value;
            ChangeValue(change);
        }

        public virtual void ChangeValue(float change)
        {
            if (change == 0)
                return;

            float changeVolume = Mathf.Abs(change);
            if (change > 0)
                IncreaseValue(changeVolume);
            else
                DecreaseValue(changeVolume);
        }

        protected virtual void IncreaseValue(float amount)
        {
            currentValue += amount;
            NotifyValueChange();
        }

        protected virtual void DecreaseValue(float amount)
        {
            currentValue -= amount;
            NotifyValueChange();
        }

        private void NotifyValueChange()
        {
            OnValueChanged?.Invoke(Value);
            OnValueChangedNoArgs?.Invoke();
        }
    }
}