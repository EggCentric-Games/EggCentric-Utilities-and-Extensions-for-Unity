using EggCentric.DataContainers;
using System.Collections.Generic;
using UnityEngine;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityHandler : MonoBehaviour, IGravityAffectedBody
    {
        public ITrackableValue<bool> IsInZeroGravity => _isInZeroGravity;

        [SerializeField] private Rigidbody2D _body;

        private readonly List<IForceFieldContext> _gravityFields = new();
        private readonly Field<bool> _isInZeroGravity = new(true);

        public void ApplyGravity(IForceFieldContext context)
        {
            _gravityFields.Add(context);
            UpdateZeroGravityState();
        }

        public void RemoveGravity(IForceFieldContext context)
        {
            _gravityFields.Remove(context);
            UpdateZeroGravityState();
        }

        private void FixedUpdate()
        {
            foreach (var context in _gravityFields)
                _body.AddForce(context.EvaluateForPosition(transform.position) * _body.mass, ForceMode2D.Force);
        }

        private void UpdateZeroGravityState() => _isInZeroGravity.Value = _gravityFields.Count <= 0;
    }
}