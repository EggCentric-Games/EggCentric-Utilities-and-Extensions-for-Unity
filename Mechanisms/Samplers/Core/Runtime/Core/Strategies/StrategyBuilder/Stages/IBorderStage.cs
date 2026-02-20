namespace EggCentric.Samplers
{
    public interface IBorderStage
    {
        public IFinalStage Fixed();
        public IApplicationStage Looped();
        public IApplicationStage PingPong();
    }
}