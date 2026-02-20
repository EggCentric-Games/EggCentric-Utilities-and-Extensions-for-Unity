namespace EggCentric.LifeCycleHandling
{
    public static class TickingStrategies
    {
        public static TimeBasedTicking TimeBasedTicking = new TimeBasedTicking();
        public static TurnBasedTicking TurnBasedTicking = new TurnBasedTicking();
    }
}
