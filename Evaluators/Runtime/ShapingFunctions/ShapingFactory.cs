namespace EggCentric.Evaluators
{

    namespace EggCentric.Evaluators
    {
        public class ShapingFactory
        {
            public static IShapeFunction Create(ShapingType type)
            {
                switch (type)
                {
                    case ShapingType.Dot:
                        return new DotShaping();

                    case ShapingType.AbsoluteDot:
                        return new AbsoluteDotShaping();

                    case ShapingType.OneSidedDot:
                        return new OneSidedDotShaping();

                    default:
                        return new DefaultShaping();
                }
            }
        }
    }
}