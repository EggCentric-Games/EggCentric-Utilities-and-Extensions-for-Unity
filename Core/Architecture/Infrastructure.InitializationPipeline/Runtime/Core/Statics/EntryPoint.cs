using UnityEngine;

namespace EggCentric.Infrastructure.InitializationPipeline
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeGame()
        {
            API.Game = CompositionRootProvider.Instance.Build();
            API.Game.Run();
        }
    }
}