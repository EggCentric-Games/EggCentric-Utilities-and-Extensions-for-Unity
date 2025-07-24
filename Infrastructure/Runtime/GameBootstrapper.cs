using EggCentric.Singletons;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EggCentric.Infrastructure
{
    public class GameBootstrapper : Singleton<GameBootstrapper>
    {
        private const string _GameScene = "SampleScene";

        public void InitializeGame()
        {
            InitializeSystems();
            LoadInitialScene();
        }

        private void InitializeSystems()
        {

        }

        private void LoadInitialScene()
        {
            var loading = SceneManager.LoadSceneAsync(_GameScene);
            loading.completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperation operation)
        {

        }
    }
}