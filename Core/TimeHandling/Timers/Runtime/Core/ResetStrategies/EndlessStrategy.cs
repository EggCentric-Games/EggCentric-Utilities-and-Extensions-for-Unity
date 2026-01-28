namespace EggCentric.Timers
{
    public class EndlessStrategy : IResetStrategy
    {
        public bool IsFinal() => false;
    }
}