using UnityEngine;

namespace EggCentric.StateMachines
{
    public class DelayedPolicy : ExecutionPolicy
    {
        public override bool IsValid => !isExpired;

        private float _lifetime;
        private float _creationTime;

        private bool isExpired => _lifetime < 0 ? false : (Time.time - _creationTime) > _lifetime;

        public DelayedPolicy(float lifetime = -1f)
        {
            _lifetime = lifetime;
            _creationTime = Time.time;
        }
    }
}