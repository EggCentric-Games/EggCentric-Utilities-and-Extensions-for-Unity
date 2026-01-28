using System;
using UnityEngine;

namespace EggCentric.Stats
{
    public class ValueStat : IStat
    {
        protected float currentValue;

        public float TotalValue => currentValue;

        public event Action OnValueChanged;

        public ValueStat(float value = 0f)
        {
            this.currentValue = value;
        }

        public virtual void SetValue(float value)
        {
            float change = value - TotalValue;
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

            OnValueChanged?.Invoke();
        }

        protected virtual void DecreaseValue(float amount)
        {
            currentValue -= amount;

            OnValueChanged?.Invoke();
        }
    }
}