using System;

namespace EggCentric.Stats
{
    public interface IStat
    {
        public float TotalValue { get; }

        public event Action OnValueChanged;

        public void SetValue(float value);
        public void ChangeValue(float change);
    }
}