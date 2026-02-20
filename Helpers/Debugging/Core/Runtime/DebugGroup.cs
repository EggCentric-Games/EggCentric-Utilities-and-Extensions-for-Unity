using UnityEngine;

namespace EggCentric.Debugging
{
    [CreateAssetMenu(fileName = "DebugGroup", menuName = "EggCentric/Debugging/DebugGroup")]
    public class DebugGroup : ScriptableObject
    {
        public bool IsEnabled => _isEnabled || (_parent != null && _parent.IsEnabled);

        [SerializeField] private DebugGroup _parent;
        [SerializeField] private bool _isEnabled;
    }
}