namespace EggCentric.Timers
{
    public static class ResetStrategies
    {
        public static IResetStrategy Once = new OneShotStrategy();
        public static IResetStrategy Endless = new EndlessStrategy();
    }
}