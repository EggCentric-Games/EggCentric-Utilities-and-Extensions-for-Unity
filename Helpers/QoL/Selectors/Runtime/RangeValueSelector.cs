using EggCentric.Randomization;
using UnityEngine;

namespace EggCentric.QoL.Selectors
{
    [System.Serializable]
    public class RangeValueSelector : INumberSelector
    {
        public float Value => RandomTypes.ByType(_randomType).Range(_range.x, _range.y);

        [SerializeField] private Vector2 _range;
        [SerializeField] private RandomType _randomType;

        public void Validate() => _range.x = Mathf.Min(_range.x, _range.y);
    }
}