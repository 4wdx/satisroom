using System.Threading.Tasks;
using CodeBase.Gameplay.Levels;
using CodeBase.Gameplay.Mechanics;
using CodeBase.Gameplay.UI;
using CodeBase.Root;
using UnityEngine;
using UnityEngine.Serialization;
using YG;

namespace CodeBase.Gameplay.Root
{
    public class GameplayMediator : MonoBehaviour
    {
        [field: SerializeField] public Sprite LevelIcon { get; private set; }

        [FormerlySerializedAs("_levelRool"),SerializeField] private Level _level;
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
        }

        public void OnDestroy()
        {
            //unsub
            _level.OnCompleted -= LevelCompleteHandle;
            _uiMediator.OnShowHint -= ShowHintHandle;
            _uiMediator.OnUISceneExit -= UISceneExitHandle;
        }
        
        private void LevelCompleteHandle(bool result)
        {
            if (result == true)
                SaveManager.LevelComplete(_levelIndex);
            
            _uiMediator.ShowResultWindow(_levelIndex);
        }

        private void UISceneExitHandle(ExitType exitType) => 
            _gameplayExitInvoker.InvokeExit(exitType);

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