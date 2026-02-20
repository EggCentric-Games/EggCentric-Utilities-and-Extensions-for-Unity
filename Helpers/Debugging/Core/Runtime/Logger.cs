namespace EggCentric.Debugging
{
    public abstract class Logger
    {
        public bool IsEnabled => _debugGroup.IsEnabled;

        private DebugGroup _debugGroup;

        public Logger(DebugGroup group) => _debugGroup = group;
    }
}