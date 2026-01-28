using System;

namespace EggCentric.StateMachines
{
    [Flags]
    public enum TransitionFlags
    {
        None = 0,
        IgnoreLocks = 1 << 0,
        IgnoreConditions = 1 << 1
    }
}