using UnityEngine;

namespace EggCentric.Infrastructure
{
    public static class EntryPoint
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeGame() => CompositionRootProvider.Instance.Build().Run();
    }
}