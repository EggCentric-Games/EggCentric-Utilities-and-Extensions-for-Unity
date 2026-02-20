using UnityEngine;

namespace EggCentric.ForceFields.Gravity
{
    public class GravityHandler : ForceFieldsReceiver<IGravityFieldContext>, IGravityAffectedBody
    {
        public bool IsInZeroGravity => !HasActiveFields.Value || TotalForce.Value.magnitude < _zeroGravityThreshold;

        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private float _zeroGravityThreshold = 0.01f;

        private void FixedUpdate() => _body.AddForce(TotalForce.Value * _body.mass);
    }
}