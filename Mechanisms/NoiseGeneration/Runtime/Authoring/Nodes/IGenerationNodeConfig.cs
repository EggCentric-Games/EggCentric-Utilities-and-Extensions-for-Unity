namespace EggCentric.ProceduralGeneration
{
    public interface IGenerationNodeConfig<out TNode> : IConfig<TNode> where TNode : IGenerationNode
    {
        public TNode CreateInstance();
    }
}
