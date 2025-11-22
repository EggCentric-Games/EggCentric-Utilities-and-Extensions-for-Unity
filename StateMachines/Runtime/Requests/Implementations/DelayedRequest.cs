using UnityEngine;

namespace EggCentric.StateMachines
{
    public class DelayedRequest : TransitionRequest
    {
        public override bool IsValid => meantTransition.IsSatisfied && !isExpired;

        private float _lifetime;
        private float _creationTime;

        private bool isExpired => _lifetime < 0 ? false : (Time.time - _creationTime) > _lifetime;

        public DelayedRequest(Transition meantTransition, float lifetime = -1f) : base(meantTransition)
        {
            _lifetime = lifetime;
            _creationTime = Time.time;
        }
    }
}