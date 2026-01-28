namespace EggCentric.Effects.Sources
{
    public static class EffectApplicationFilters
    {
        public static IEffectApplicationFilter All => new AllRequired();
        public static IEffectApplicationFilter Any => new AnyAccepted();
        public static IEffectApplicationFilter Always => new AlwaysValid();
    }
}