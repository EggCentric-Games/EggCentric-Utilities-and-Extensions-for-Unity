using System;
using UnityEngine;

namespace EggCentric.StateMachines
{
    public class DelayedRequest : TransitionRequest
    {
        public override bool IsValid => !isExpired;

        private float _lifetime;
        private float _creationTime;

        private bool isExpired => _lifetime < 0 ? false : (Time.time - _creationTime) > _lifetime;

        public DelayedRequest(Type targetState, object source, float lifetime = -1f, int priority = 0) : base(targetState, source, priority)
        {
            _lifetime = lifetime;
            _creationTime = Time.time;
        }
    }
}