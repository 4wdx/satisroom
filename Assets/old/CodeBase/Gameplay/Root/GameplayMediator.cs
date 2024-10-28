using System.Collections.Generic;

using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.UI;
using CodeBase.Root;
using CodeBase.Utils;

using UnityEngine;
using YG;

namespace CodeBase.Gameplay.Root
{
    public class GameplayMediator : MonoBehaviour
    {
        [field: SerializeField] public Sprite LevelIcon { get; private set; }

        [SerializeField] private Level _level;
        [SerializeField] private GameObject _hint;
        
        private GameplayExitInvoker _gameplayExitInvoker;
        private GameplayUIMediator _uiMediator;
        private int _levelIndex;
        
        public void Initialize(GameplayExitInvoker gameplayExitInvoker, GameplayUIMediator uiMediator, int levelIndex)
        {
            _gameplayExitInvoker = gameplayExitInvoker;
            _uiMediator = uiMediator;
            _levelIndex = levelIndex;
            
            //sub
            _level.OnCompleted += LevelCompleteHandle;
            _uiMediator.OnShowHint += ShowHintHandle;
            _uiMediator.OnUISceneExit += UISceneExitHandle;
            _uiMediator.OnForcedCloseLevel += ForcedCloseLevelHandle;
        }

        public void OnDestroy()
        {
            //unsub
            _level.OnCompleted -= LevelCompleteHandle;
            _uiMediator.OnShowHint -= ShowHintHandle;
            _uiMediator.OnUISceneExit -= UISceneExitHandle;
            _uiMediator.OnForcedCloseLevel -= ForcedCloseLevelHandle;
        }

        private void LevelCompleteHandle(bool result)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            if (result == true)
            {
                SaveManager.LevelComplete(_levelIndex);
                
                data.Add(Const.LevelWinMetricaName, _levelIndex.ToString());
                YandexMetrica.Send(Const.LevelWinMetricaName, data);
            }
            else
            {
                data.Add(Const.LevelLoseMetricaName, _levelIndex.ToString());
                YandexMetrica.Send(Const.LevelLoseMetricaName, data);
            }
            
            _uiMediator.ShowResultWindow(_levelIndex);
        }

        private void UISceneExitHandle(ExitType exitType) => 
            _gameplayExitInvoker.InvokeExit(exitType);

        private void ForcedCloseLevelHandle()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add(Const.ForcedCloseLevelMetricaName, _levelIndex.ToString());
            YandexMetrica.Send(Const.ForcedCloseLevelMetricaName, data);
            
            _gameplayExitInvoker.InvokeExit(ExitType.ToMainMenu);
        }
        
        private void ShowHintHandle()
        {
            YandexGame.RewVideoShow(99);
            YandexGame.RewardVideoEvent += RewardedAwait;
        }

        private void RewardedAwait(int id)
        {
            if (id == 99) _hint.SetActive(true); //show if showing rewarded
            
            YandexGame.RewardVideoEvent -= RewardedAwait;
        }
    }
}