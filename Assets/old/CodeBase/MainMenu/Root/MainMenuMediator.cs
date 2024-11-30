using UnityEngine;
using YG;

namespace CodeBase.MainMenu.Root
{
    public class MainMenuMediator : MonoBehaviour
    {
        private MainMenuExitInvoker _mainMenuExitInvoker;
        private PageHandler _pageHandler;
        private TikTokFeed _tikTokFeed;
     
        
        public void Initialize(MainMenuExitInvoker mainMenuExitInvoker, TikTokFeed tikTokFeed, PageHandler pageHandler)
        {
            _mainMenuExitInvoker = mainMenuExitInvoker;
            _pageHandler = pageHandler;
            _tikTokFeed = tikTokFeed;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
                YandexGame.FullscreenShow();
        }

        public void StartGameplay(int targetLevel) => 
            _mainMenuExitInvoker.Invoke(targetLevel);

        public void ReconstructButtons() => 
            _pageHandler.ReconstructPages();
    }
}