using EggCentric.PID;
using System;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] private Rigidbody _body;
    [SerializeField] private float _accelerationForce;

    private float _throttle;

    public void SetThrottle(float throttle)
    {
        _throttle = Mathf.Clamp(throttle, -1f, 1f);
    }

    private void FixedUpdate()
    {
        _body.AddForce(Vector2.right * _accelerationForce * _throttle, ForceMode.Force);
    }
}

public class TrainController : PidController<float>
{
    public TrainController(Func<float> getter) : base(getter)
    {
    }

    protected override float GetError(float value) => target - value;
}
