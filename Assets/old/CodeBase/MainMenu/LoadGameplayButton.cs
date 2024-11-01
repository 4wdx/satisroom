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
    public class LoadGameplayButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _lockPanel;
        [SerializeField] private GameObject _completedPanel;
        
        private MainMenuMediator _mainMenuMediator;
        private Image _lockImage;
        private int _levelIndex;

        private void Awake() => 
            _lockImage = _lockPanel.GetComponent<Image>();

        private void Start() => 
            _mainMenuMediator = ServiceLocator.Resolve<MainMenuMediator>();

        public void ReConstruct(int levelIndex, Sprite levelIcon)
        {
            _levelIndex = levelIndex;
            _image.sprite = levelIcon;
            _lockImage.sprite = levelIcon;

            _lockPanel.SetActive(!SaveManager.IsOpened(_levelIndex));
            _completedPanel.SetActive(SaveManager.IsCompleted(_levelIndex));
        }
        
        public void LoadGameplay()
        {
            if (SaveManager.IsOpened(_levelIndex))
                _mainMenuMediator.StartGameplay(_levelIndex);
            else
            {
                YandexGame.RewVideoShow(_levelIndex);
                YandexGame.RewardVideoEvent += RewardedAwait;
            }
        }

        private void RewardedAwait(int id)
        {
            if (id == _levelIndex)
                SaveManager.OpenLevel(_levelIndex);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add(Const.OpenLevelMetricaName, _levelIndex.ToString());
            YandexMetrica.Send(Const.OpenLevelMetricaName, data);
            
            _mainMenuMediator.ReconstructButtons();
            YandexGame.RewardVideoEvent -= RewardedAwait;
        }
    }
}