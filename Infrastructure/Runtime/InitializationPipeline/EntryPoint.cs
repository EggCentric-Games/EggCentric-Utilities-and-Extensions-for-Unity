using EggCentric.Singletons;

namespace EggCentric.Infrastructure
{
    public class EntryPoint : MonoSingleton<EntryPoint>
    {
        public GameBootstrapper Bootstrapper;

        protected override void Awake()
        {
            base.Awake();

            SetupBootstrapper();
        }

        private void SetupBootstrapper()
        {
            Bootstrapper = new GameBootstrapper();
            Bootstrapper.InitializeGame();
        }
    }
}