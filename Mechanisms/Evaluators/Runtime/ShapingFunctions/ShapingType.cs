namespace EggCentric.Evaluators
{
    //Created due to inability serialize interfaces in default Unity inspector 
    //Perhaps, would be replaced by automatic solution in future, probably by using reflection 
    public enum ShapingType
    {
        None = 0,
        Dot,
        AbsoluteDot,
        OneSidedDot
    }
}
