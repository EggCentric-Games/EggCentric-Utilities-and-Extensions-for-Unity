using UnityEngine;

namespace EggCentric.QoL.Selectors
{
    [System.Serializable]
    public class FixedValueSelector : INumberSelector
    {
        public float Value => _value;

        [SerializeField] private float _value;

        public void Validate() { }
    }
}