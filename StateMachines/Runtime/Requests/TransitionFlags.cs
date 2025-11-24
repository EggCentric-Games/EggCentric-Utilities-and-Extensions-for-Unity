using System;

namespace EggCentric.StateMachines
{
    [Flags]
    public enum TransitionFlags
    {
        None = 0,
        Forced = 1 << 0
    }
}