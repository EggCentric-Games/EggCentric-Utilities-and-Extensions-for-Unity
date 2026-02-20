namespace EggCentric.Samplers
{
    public class StrategyBuilder : IBorderStage, IApplicationStage, IFinalStage
    {
        private ISegmentationPolicy _segmentationPolicy;

        private IBorderStrategy _borderStrategy;
        private ITimeProcessor _timeProcessor;
        private IApplicationStrategy _applicationStrategy;

        public static IBorderStage CreateBase() => new StrategyBuilder().WithPolicy(new DefaultSegmentationPolicy());

        public IBorderStage WithPolicy(ISegmentationPolicy segmentationPolicy)
        {
            _segmentationPolicy = segmentationPolicy;
            return this;
        }

        public IFinalStage Fixed()
        {
            _borderStrategy = new BorderlessStrategy();
            _timeProcessor = new Quantizer();
            _applicationStrategy = new Linear();
            return this;
        }

        public IApplicationStage Looped()
        {
            _borderStrategy = new LoopStrategy();
            return this;
        }

        public IApplicationStage PingPong()
        {
            _borderStrategy = new PingPongStrategy();
            return this;
        }

        public IFinalStage Cumulative()
        {
            _timeProcessor = new Quantizer();
            _applicationStrategy = new Cumulative(_borderStrategy);

            return this;
        }

        public IFinalStage Repetative()
        {
            _timeProcessor = new StepNumber();
            _applicationStrategy = new Repetative(_borderStrategy, _segmentationPolicy.GetSegmentation(_borderStrategy));

            return this;
        }

        public SamplingStrategy Build() => new SamplingStrategy(_borderStrategy, _timeProcessor, _applicationStrategy);
    }
}