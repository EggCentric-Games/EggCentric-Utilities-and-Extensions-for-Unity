namespace EggCentric.Timers
{
    public class OneShotStrategy : IResetStrategy
    {
        public bool IsFinal() => true;
    }
}