namespace EggCentric.Samplers
{
    public interface IApplicationStage
    {
        public IFinalStage Cumulative();
        public IFinalStage Repetative();
    }
}