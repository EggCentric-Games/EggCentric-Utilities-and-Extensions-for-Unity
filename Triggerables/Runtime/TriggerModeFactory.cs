namespace EggCentric.Triggerables
{
    public class TriggerModeFactory
    {
        public static ITriggerStrategy Create(TriggerMode mode, Trigger owner)
        {
            switch (mode)
            {
                case TriggerMode.Single:
                    return new SingleStrategy(owner);

                case TriggerMode.Querry:
                    return new QueryStrategy(owner);

                case TriggerMode.Free:
                    return new FreeStrategy(owner);

                case TriggerMode.Volley:
                    return new VolleyStrategy(owner);

                default:
                    return new SingleStrategy(owner);
            }
        }
    }
}