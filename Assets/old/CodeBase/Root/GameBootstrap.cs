using UnityEngine;

namespace CodeBase.Root
{
    public static class GameBootstrap
    {
        private static Game _game;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void InitializeOnLoad()
        {
            SetApplicationSettings();
            
            _game = new Game();
        }

        private static void SetApplicationSettings()
        {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }
    }
}
