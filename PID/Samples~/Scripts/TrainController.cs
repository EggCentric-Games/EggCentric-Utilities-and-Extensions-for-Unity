using EggCentric.PID;
using System;

public class TrainController : PidController<float>
{
    public TrainController(Func<float> getter) : base(getter)
    {
    }

    protected override float GetError(float value) => target - value;
}
