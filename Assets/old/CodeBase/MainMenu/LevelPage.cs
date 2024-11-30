using System.Collections.Generic;
using CodeBase.MainMenu.Root;
using CodeBase.Root;
using CodeBase.ServiceLocatorAPI;
using CodeBase.Utils;
using UnityEngine;
using UnityEngine.UI;

using YG;

namespace CodeBase.MainMenu
{
    public class LevelPage : MonoBehaviour
    {
        [SerializeField] private GameObject _stars;
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _openButton;
        
        private MainMenuMediator _mainMenuMediator;
        [SerializeField] private int _levelIndex;

        private void Start()
        {
            _mainMenuMediator = ServiceLocator.Resolve<MainMenuMediator>();
            _playButton.onClick.AddListener(StartGameplay);
            _openButton.onClick.AddListener(LoadGameplay);
        }
        
        public void Initialize(int levelIndex) => 
            _levelIndex = levelIndex;

        public void OnEnable()
        {
            _openButton.gameObject.SetActive(!SaveManager.IsOpened(_levelIndex));
            _playButton.gameObject.SetActive(SaveManager.IsOpened(_levelIndex));
            _stars.SetActive(SaveManager.IsCompleted(_levelIndex));
        }

        private void StartGameplay() => 
            _mainMenuMediator.StartGameplay(_levelIndex);

        public void LoadGameplay()
        {
            YandexGame.RewVideoShow(_levelIndex);
            YandexGame.RewardVideoEvent += RewardedAwait;
        }

        private void RewardedAwait(int id)
        {
            if (id == _levelIndex)
                SaveManager.OpenLevel(_levelIndex);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add(Const.OpenLevelMetricaName, _levelIndex.ToString());
            YandexMetrica.Send(Const.OpenLevelMetricaName, data);
            
            _mainMenuMediator.StartGameplay(_levelIndex);
            YandexGame.RewardVideoEvent -= RewardedAwait;
        }
    }
}