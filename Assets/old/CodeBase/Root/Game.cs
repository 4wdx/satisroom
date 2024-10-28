using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

using CodeBase.Cfg;
using CodeBase.Gameplay.Root;
using CodeBase.MainMenu.Root;
using CodeBase.ServiceLocatorAPI;
using CodeBase.Utils;

using YG;

namespace CodeBase.Root
{
    public class Game
    {
        private Coroutines _coroutines;
        private UIRoot _uiRoot;

        public Game()
        {
            CreateComponents();
            _coroutines.StartCoroutine(StartGame());
        }

        private void CreateComponents()
        {
            _coroutines = new GameObject("[Coroutines]").AddComponent<Coroutines>();
            Object.DontDestroyOnLoad(_coroutines);
            ServiceLocator.Register(_coroutines);
            
            var prefab = Resources.Load<UIRoot>("UIRoot");
            _uiRoot = Object.Instantiate(prefab);
            Object.DontDestroyOnLoad(_uiRoot);
            ServiceLocator.Register(_uiRoot);

            var levelQueue = Resources.Load<LevelQueue>("MainLevelQueue");
            ServiceLocator.Register(levelQueue);
        }

        private IEnumerator StartGame()
        {
            YandexMetrica.Send(Const.StartGameMetricaName);
            
/*#if UNITY_EDITOR
            string loadingScene = SceneManager.GetActiveScene().name;
            
            _uiRoot.ShowLoadingScreen();
            yield return SceneManager.LoadSceneAsync(SceneNames.BOOT);
            
            while (YandexGame.SDKEnabled == false)
                yield return null;
            
            yield return SceneManager.LoadSceneAsync(loadingScene);
            yield break;
#endif*/
            
            _uiRoot.ShowLoadingScreen();
            yield return SceneManager.LoadSceneAsync(SceneNames.BOOT);
            
            while (YandexGame.SDKEnabled == false)
                yield return null;
            
            _coroutines.StartCoroutine(LoadMainMenu());
            YandexGame.FullscreenShow();
        }

        private IEnumerator LoadMainMenu()
        {
            ServiceLocator.DisposeScene();
            _uiRoot.ClearChildUI();
            _uiRoot.ShowLoadingScreen();

            yield return SceneManager.LoadSceneAsync(SceneNames.BOOT);
            yield return SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);

            MainMenuBootstrap menuBootstrap = Object.FindFirstObjectByType<MainMenuBootstrap>();
            MainMenuExitInvoker exitParams = menuBootstrap.Run();
            Debug.Log(exitParams);
            exitParams.OnMenuExit += OnMenuExitHandle;
            YandexGame.GameplayStop();
            
            _uiRoot.HideLoadingScreen();
        }

        private void OnMenuExitHandle(int levelIndex) => 
            _coroutines.StartCoroutine(LoadGameplay(levelIndex));

        private IEnumerator LoadGameplay(int levelIndex)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add(Const.LevelStartMetricaName, levelIndex.ToString());
            YandexMetrica.Send(Const.LevelStartMetricaName, data);
            
            ServiceLocator.DisposeScene();
            _uiRoot.ClearChildUI();
            _uiRoot.ShowLoadingScreen();

            yield return SceneManager.LoadSceneAsync(SceneNames.BOOT);
            yield return SceneManager.LoadSceneAsync(SceneNames.GAMEPLAY);

            GameplayBootstrap menuBootstrap = Object.FindFirstObjectByType<GameplayBootstrap>();
            GameplayExitInvoker exitParams = menuBootstrap.Run(levelIndex);
            exitParams.OnGameplayExit += OnGameplayExitHandle;
            YandexGame.GameplayStart();
            
            _uiRoot.HideLoadingScreen();
        }

        private void OnGameplayExitHandle(GameplayExitParams gameplayExitParams)
        {
            
            switch (gameplayExitParams.ExitType)
            {
                case ExitType.ToMainMenu:
                    _coroutines.StartCoroutine(LoadMainMenu());
                    break;
                
                case ExitType.Restart:
                    _coroutines.StartCoroutine(LoadGameplay(gameplayExitParams.LevelIndex));
                    break;    
                
                case ExitType.ToNext:
                    if (SaveManager.IsOpened(gameplayExitParams.LevelIndex + 1))
                        _coroutines.StartCoroutine(LoadGameplay(gameplayExitParams.LevelIndex + 1));
                    else
                        throw new Exception("Trying to open closed level after gameplay");
                    break;
                
                default:
                    throw new Exception($"ExitEnum Type {gameplayExitParams.ExitType} does not implemented");
            }
        }
    }
}